using GTeams_backend.Data;
using GTeams_backend.Dtos.ColaboradorDtos;
using GTeams_backend.Dtos.EmailDtos;
using GTeams_backend.Dtos.EquipeDtos;
using GTeams_backend.Dtos.MatriculaDtos;
using GTeams_backend.Extensions;
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
            .Include(c => c.Emails)
            .Include(c => c.EquipesColaboradores)
            .ThenInclude(ec => ec.Equipe)
            .ToListAsync();

        var matriculasInformadas = inserirColaboradorDto.Matriculas
            .Select(m => m.Codigo.Trim().ToLower())
            .ToHashSet();

        foreach (var existente in colaboradores)
        {
            bool nomesIguais = existente.Nome.Trim().ToLower() == inserirColaboradorDto.Nome.Trim().ToLower();
            bool matriculaCoincidente = existente.Matriculas.Any(m => matriculasInformadas.Contains(m.Codigo.Trim().ToLower()));

            if (nomesIguais && matriculaCoincidente)
            {
                if (existente.Ativo)
                    throw new InvalidOperationException("Já existe um colaborador com o mesmo nome e matrícula no sistema.");

                // Reativar
                existente.Ativo = true;
                existente.Funcao = inserirColaboradorDto.Funcao;
                existente.SetPassword(inserirColaboradorDto.Password);

                await appDbContext.SaveChangesAsync();

                return existente.ToReturnDto();
            }
        }

        // Criar colaborador novo
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
        foreach (var matricula in inserirColaboradorDto.Matriculas)
        {
            matricula.ColaboradorId = colaborador.Id;
            await matriculaService.InserirMatriculaAsync(matricula);
        }

        // Adicionar e-mails
        foreach (var email in inserirColaboradorDto.Emails)
        {
            email.ColaboradorId = colaborador.Id;
            await emailService.InserirEmailAsync(email);
        }

        await appDbContext.SaveChangesAsync();

        // Recuperar colaborador completo
        var colaboradorComDados = await appDbContext.Colaboradores
            .Include(c => c.Emails)
            .Include(c => c.Matriculas)
            .Include(c => c.EquipesColaboradores)
            .ThenInclude(ec => ec.Equipe)
            .FirstOrDefaultAsync(c => c.Id == colaborador.Id);

        if (colaboradorComDados == null)
            throw new InvalidOperationException("Erro ao recuperar colaborador.");

        return colaboradorComDados.ToReturnDto();
    }

    public async Task<Colaborador?> ObterColaboradorPorIdAsync(int colaboradorId)
    {
        return await appDbContext.Colaboradores
            .Include(c => c.Matriculas)
            .Include(c => c.Emails)
            .Include(c => c.EquipesColaboradores)
            .ThenInclude(ec => ec.Equipe)
            .Include(c => c.DatasPersonalizadasColaborador)
            .Include(c => c.Observacoes)
            .FirstOrDefaultAsync(c => c.Id == colaboradorId);
    }
    
    public async Task<RetornarColaboradorDto> DesativarColaboradorAsync(int colaboradorId)
    {
        Colaborador? colaborador = await ObterColaboradorPorIdAsync(colaboradorId);
        
        if (colaborador == null)
            throw new KeyNotFoundException("Colaborador não encontrado.");
        if (!colaborador.Ativo)
            throw new InvalidOperationException("O colaborador já está desativado");
        
        colaborador.Ativo = false;
        await appDbContext.SaveChangesAsync();

        return colaborador.ToReturnDto();
    }
    
    public async Task<List<Colaborador>> ObterTodosColaboradoresAsync()
    {
        return await appDbContext.Colaboradores
            .Include(c => c.Matriculas)
            .Include(c => c.Emails)
            .Include(c => c.EquipesColaboradores)
            .ThenInclude(ec => ec.Equipe)
            .ToListAsync();
    }
    
    public async Task<Colaborador?> ObterColaboradorPorMatriculaAsync(string matricula)
    {
        var colaborador = await appDbContext.Colaboradores
            .Include(c => c.Matriculas)
            .Include(c => c.Emails)
            .Include(c => c.EquipesColaboradores)
            .ThenInclude(ec => ec.Equipe)
            .Include(c => c.DatasPersonalizadasColaborador)
            .Include(c => c.Observacoes)
            .FirstOrDefaultAsync(c => c.Matriculas.Any(m => m.Codigo.Trim().ToLower() == matricula.Trim().ToLower()));

        if (colaborador is null)
            return null;

        return colaborador;
    }
}