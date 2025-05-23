using GTeams_backend.Data;
using GTeams_backend.GestaoPessoas.Dtos.ColaboradorDtos;
using GTeams_backend.GestaoPessoas.Dtos.EmailDtos;
using GTeams_backend.GestaoPessoas.Dtos.MatriculaDtos;
using GTeams_backend.GestaoPessoas.Extensions;
using GTeams_backend.GestaoPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoPessoas.Services;

public class ColaboradorService(AppDbContext appDbContext, EmailService emailService, MatriculaService matriculaService)
{
    public async Task<RetornarColaboradorDto> InserirAsync(InserirColaboradorDto inserirColaboradorDto)
    {
        Colaborador? existente = await ObterPorUserAsync(inserirColaboradorDto.User);

        if (existente != null )
        {
                throw new InvalidOperationException("Colaborador já existe no sistema.");
        }
        
        Colaborador colaborador = new Colaborador
        {
            Nome = inserirColaboradorDto.Nome,
            User = inserirColaboradorDto.User,
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
            await matriculaService.InserirAsync(matricula);
        }

        // Adicionar e-mails
        foreach (InserirEmailDto email in inserirColaboradorDto.Emails)
        {
            email.ColaboradorId = colaborador.Id;
            await emailService.InserirAsync(email);
        }

        await appDbContext.SaveChangesAsync();

        // Recuperar colaborador completo
        Colaborador? colaboradorComDados = await ObterPorUserAsync(colaborador.User);

        if (colaboradorComDados == null)
            throw new InvalidOperationException("Erro ao recuperar colaborador.");

        return colaboradorComDados.ToReturnDto();
    }

    public async Task<Colaborador?> ObterPorIdAsync(int colaboradorId)
    {
        return await appDbContext.Colaboradores
            .Include(c => c.Matriculas)
            .Include(c => c.Emails)
            .Include(c => c.Observacoes)
            .FirstOrDefaultAsync(c => c.Id == colaboradorId);
    }
    
    public async Task<RetornarColaboradorDto> DesativarAsync(int colaboradorId)
    {
        Colaborador? colaborador = await ObterPorIdAsync(colaboradorId);
        
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
            .ToListAsync();
    }
    
    public async Task<Colaborador?> ObterPorUserAsync(string user)
    {
        var colaborador = await appDbContext.Colaboradores
            .Include(c => c.Matriculas)
            .Include(c => c.Emails)
            .Include(c => c.Observacoes)
            .FirstOrDefaultAsync(c => c.User.Trim().ToLower() == user.Trim().ToLower());

        if (colaborador is null)
            return null;

        return colaborador;
    }
}