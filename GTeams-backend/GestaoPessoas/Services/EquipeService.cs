using GTeams_backend.Data;
using GTeams_backend.GestaoPessoas.Dtos.EquipeDtos;
using GTeams_backend.GestaoPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoPessoas.Services;

public class EquipeService(AppDbContext appDbContext)
{
    public async Task<RetornarEquipeDto> InserirEquipe(InserirEquipeDto equipe)
    {
        Equipe? buscaEquipe = await appDbContext.Equipes.FirstOrDefaultAsync(e => e.Nome.ToLower() == equipe.Nome.ToLower());

        if (buscaEquipe is { Ativo: false })
        {
            buscaEquipe.Ativo = true;
            await appDbContext.SaveChangesAsync();
            return new RetornarEquipeDto
            {
                Id = buscaEquipe.Id,
                Nome = buscaEquipe.Nome,
                Ativo = buscaEquipe.Ativo,
            };
        }
        else if (buscaEquipe != null)
            throw new InvalidOperationException("Esta equipe já está cadastrada.");
        
        Equipe novaEquipe = new Equipe
        {
            Nome = equipe.Nome
        };
        
        await appDbContext.Equipes.AddAsync(novaEquipe);
        await appDbContext.SaveChangesAsync();

        return new RetornarEquipeDto
        {
            Id = novaEquipe.Id,
            Nome = novaEquipe.Nome,
            Ativo = novaEquipe.Ativo
        };
    }

    public async Task<RetornarEquipeDto> DesativarEquipeAsync(int equipeId)
    {
        Equipe? equipe = await appDbContext.Equipes.FindAsync(equipeId);

        if (equipe == null)
            throw new KeyNotFoundException("Equipe não encontrada.");
        
        if (!equipe.Ativo)
            throw new InvalidOperationException("A equipe já está desativada.");
        
        equipe.Ativo = false;
        await appDbContext.SaveChangesAsync();

        return new RetornarEquipeDto
        {
            Id = equipe.Id,
            Nome = equipe.Nome,
            Ativo = equipe.Ativo
        };
    }

    public async Task<Equipe?> ObterEquipePorIdAsync(int equipeId)
    {
        Equipe? equipe = await appDbContext.Equipes.FindAsync(equipeId);
        return equipe;
    }
    
    public async Task<List<Equipe>> ObterTodasEquipesAsync()
    {
        return await appDbContext.Equipes.ToListAsync();
    }

}