using GTeams_backend.Data;
using GTeams_backend.Dtos.ColaboradorDtos;
using GTeams_backend.Dtos.EmailDtos;
using GTeams_backend.Dtos.EquipeDtos;
using GTeams_backend.Dtos.MatriculaDtos;
using GTeams_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.Services;

public class ColaboradorService(AppDbContext appDbContext, EquipeService equipeService, EmailService emailService, MatriculaService matriculaService)
{
    public async Task<RetornarColaboradorDto> InserirColaboradorAsync(InserirColaboradorDto inserirColaboradorDto)
    {
        // Verificação global: todos os colaboradores
        var colaboradores = await appDbContext.Colaboradores
            .Include(c => c.Matriculas)
            .ToListAsync();

        var matriculasInformadas = inserirColaboradorDto.Matriculas
            .Select(m => m.Codigo.Trim().ToLower())
            .ToHashSet();

        foreach (var existente in colaboradores)
        {
            bool nomesIguais = existente.Nome.Trim().ToLower() == inserirColaboradorDto.Nome.Trim().ToLower();
            bool matriculaCoincidente = existente.Matriculas.Any(m => matriculasInformadas.Contains(m.Codigo.Trim().ToLower()));

            if (nomesIguais && matriculaCoincidente)
                throw new InvalidOperationException("Já existe um colaborador com o mesmo nome e matrícula no sistema.");
        }

        // Criar colaborador sem equipe
        Colaborador colaborador = new Colaborador
        {
            Nome = inserirColaboradorDto.Nome,
            Cpf = inserirColaboradorDto.Cpf,
            Funcao = inserirColaboradorDto.Funcao
        };

        colaborador.SetPassword(inserirColaboradorDto.Password);

        await appDbContext.Colaboradores.AddAsync(colaborador);
        await appDbContext.SaveChangesAsync();

        // Adicionar matrículas
        foreach (InserirMatriculaDto matricula in inserirColaboradorDto.Matriculas)
        {
            matricula.ColaboradorId = colaborador.Id;
            await matriculaService.InserirMatriculaAsync(matricula);
        }

        // Adicionar e-mails
        foreach (InserirEmailDto email in inserirColaboradorDto.Emails)
        {
            email.ColaboradorId = colaborador.Id;
            await emailService.InserirEmailAsync(email);
        }

        await appDbContext.SaveChangesAsync();

        // Recuperar colaborador completo com dados associados
        Colaborador? colaboradorComDados = await appDbContext.Colaboradores
            .Include(c => c.Emails)
            .Include(c => c.Matriculas)
            .Include(c => c.EquipesColaboradores)
            .ThenInclude(ec => ec.Equipe)
            .FirstOrDefaultAsync(c => c.Id == colaborador.Id);

        if (colaboradorComDados == null)
            throw new InvalidOperationException("Erro ao recuperar colaborador.");

        return new RetornarColaboradorDto
        {
            Id = colaboradorComDados.Id,
            Nome = colaboradorComDados.Nome,
            Cpf = colaboradorComDados.Cpf,
            Ativo = colaboradorComDados.Ativo,
            Funcao = colaboradorComDados.Funcao,
            Emails = colaboradorComDados.Emails.Select(e => new RetornarEmailDto
            {
                Descricao = e.Descricao,
                Endereco = e.Endereco
            }).ToList(),
            Matriculas = colaboradorComDados.Matriculas.Select(m => new RetornarMatriculaDto
            {
                Codigo = m.Codigo,
                Descricao = m.Descricao
            }).ToList(),
            Equipes = colaboradorComDados.EquipesColaboradores.Select(ec => new RetornarEquipeDto
            {
                Id = ec.Equipe.Id,
                Nome = ec.Equipe.Nome,
                Ativo = ec.Equipe.Ativo
            }).ToList()
        };
    }
}