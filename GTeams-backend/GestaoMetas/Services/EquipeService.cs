using GTeams_backend.Data;
using GTeams_backend.GestaoMetas.Dtos.EquipeDtos;
using GTeams_backend.GestaoMetas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoMetas.Services;

public class EquipeService(AppDbContext appDbContext)
{
    public async Task<RetornarEquipeDto> InserirAsync(InserirEquipeDto inserirEquipeDto)
    {
        Equipe? buscaEquipe = await ObterPorNomeAsync(inserirEquipeDto.Nome);
        
        if (buscaEquipe != null)
        {
            throw new InvalidOperationException("Está equipe já está cadastrada.");
        }
        
        Equipe novaEquipe = new Equipe
        {
            Nome = inserirEquipeDto.Nome
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

    public async Task<RetornarEquipeDto> DesativarAsync(int equipeId)
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

    public async Task<Equipe?> ObterPorIdAsync(int equipeId)
    {
        Equipe? equipe = await appDbContext.Equipes.FindAsync(equipeId);
        return equipe;
    }

    public async Task<Equipe?> ObterPorNomeAsync(string nome)
    {
        Equipe? equipe = await appDbContext.Equipes.FirstOrDefaultAsync(e => e.Nome.ToLower() == nome.ToLower());
        return equipe;
    }
    
    public async Task<List<Equipe>> ObterTodasAsync()
    {
        return await appDbContext.Equipes.ToListAsync();
    }

}