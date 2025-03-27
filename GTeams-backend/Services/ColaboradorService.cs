using GTeams_backend.Data;
using GTeams_backend.Dtos.ColaboradorDtos;
using GTeams_backend.Dtos.EmailDtos;
using GTeams_backend.Dtos.MatriculaDtos;
using GTeams_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.Services;

public class ColaboradorService(AppDbContext appDbContext, EquipeService equipeService, EmailService emailService, MatriculaService matriculaService)
{
    public async Task<RetornarColaboradorDto> InserirColaboradorAsync(InserirColaboradorDto inserirColaboradorDto)
    {
        //Verificar a Equipe
        Equipe? equipe = await equipeService.ObterEquipePorIdAsync(inserirColaboradorDto.EquipeId);
        if (equipe == null)
            throw new KeyNotFoundException("Equipe não encontrada.");

        
        //Inserção do Colaborador
        Colaborador colaborador = new Colaborador
        {
            Nome = inserirColaboradorDto.Nome,
            Cpf = inserirColaboradorDto.Cpf,
            Equipe = equipe,
            Funcao = inserirColaboradorDto.Funcao,
        };
        
        colaborador.SetPassword(inserirColaboradorDto.Password);
        
        await appDbContext.Colaboradores.AddAsync(colaborador);
        await appDbContext.SaveChangesAsync();

        foreach (InserirMatriculaDto matricula in inserirColaboradorDto.Matriculas)
        {
            matricula.ColaboradorId = colaborador.Id;
            await matriculaService.InserirMatriculaAsync(matricula);
        }
        foreach (InserirEmailDto email in inserirColaboradorDto.Emails)
        {
            email.ColaboradorId = colaborador.Id;
            await emailService.InserirEmailAsync(email);
        }
        
        //Verificação de integridade do banco para o objeto de retorno
        Colaborador? colaboradorComDados = await appDbContext.Colaboradores
            .Include(c => c.Equipe)
            .Include(c => c.Emails)
            .Include(c => c.Matriculas)
            .FirstOrDefaultAsync(c => c.Id == colaborador.Id);

        if (colaboradorComDados == null)
            throw new InvalidOperationException("Erro ao recuperar colaborador.");

        return new RetornarColaboradorDto
        {
            Id = colaboradorComDados.Id,
            Nome = colaboradorComDados.Nome,
            Cpf = colaboradorComDados.Cpf,
            Ativo = colaboradorComDados.Ativo,
            Equipe = colaboradorComDados.Equipe,
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
            }).ToList()
        };
    } 
}