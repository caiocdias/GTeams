using GTeams_backend.Data;
using GTeams_backend.Dtos.EquipeColaboradorDtos;
using GTeams_backend.Extensions;
using GTeams_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.Services;

public class EquipeColaboradorService(AppDbContext appDbContext, ColaboradorService colaboradorService, EquipeService equipeService) 
{
    public async Task<RetornarEquipeColaboradorDto> InserirColaboradorEmEquipeAsync(InserirEquipeColaboradorDto dto)
    {
        Colaborador? colaborador = await colaboradorService.ObterColaboradorPorIdAsync(dto.ColaboradorId);
        if (colaborador is not { Ativo: true })
            throw new InvalidOperationException("Colaborador não encontrado.");

        Equipe? equipe = await equipeService.ObterEquipePorIdAsync(dto.EquipeId);
        if (equipe is not { Ativo: true })
            throw new InvalidOperationException("Equipe não encontrada.");

        EquipeColaborador? vinculoExistente = await appDbContext.EquipesColaboradores
            .FirstOrDefaultAsync(ec => ec.ColaboradorId == dto.ColaboradorId && ec.EquipeId == dto.EquipeId);

        if (vinculoExistente != null)
        {
            if (vinculoExistente.Ativo)
                throw new InvalidOperationException("O colaborador já está vinculado a essa equipe.");
            
            vinculoExistente.Ativo = true;
            vinculoExistente.IsLider = dto.IsLider;

            await appDbContext.SaveChangesAsync();
            return vinculoExistente.ToReturnDto();
        }
        
        EquipeColaborador novoVinculo = new EquipeColaborador
        {
            ColaboradorId = dto.ColaboradorId,
            EquipeId = dto.EquipeId,
            IsLider = dto.IsLider,
            Ativo = true
        };

        appDbContext.EquipesColaboradores.Add(novoVinculo);
        await appDbContext.SaveChangesAsync();

        return novoVinculo.ToReturnDto();
    }

    public async Task<RetornarEquipeColaboradorDto> RemoverColaboradorEmEquipeAsync(int colaboradorId, int equipeId)
    {
        EquipeColaborador? vinculoExistente = await appDbContext.EquipesColaboradores
            .Include(ec => ec.Colaborador)
            .Include(ec => ec.Equipe)
            .FirstOrDefaultAsync(ec => ec.ColaboradorId == colaboradorId && ec.EquipeId == equipeId);

        if (vinculoExistente == null)
            throw new KeyNotFoundException("Vínculo entre colaborador e equipe não encontrado.");

        if (!vinculoExistente.Ativo)
            throw new InvalidOperationException("O colaborador já está desativado nesta equipe.");
        
        vinculoExistente.Ativo = false;
        await appDbContext.SaveChangesAsync();
        
        return vinculoExistente.ToReturnDto();
    }

    public async Task<EquipeColaborador?> ObterEquipeColaboradorPorIdAsync(int colaboradorId, int equipeId)
    {
        return await appDbContext.EquipesColaboradores
            .Include(ec => ec.Colaborador)
            .Include(ec => ec.Equipe)
            .FirstOrDefaultAsync(ec => ec.ColaboradorId == colaboradorId && ec.EquipeId == equipeId);
    }

    public async Task<List<EquipeColaborador>> ObterTodosEquipeColaboradorAsync()
    {
        return await appDbContext.EquipesColaboradores
            .Include(ec => ec.Colaborador)
            .Include(ec => ec.Equipe)
            .ToListAsync();
    }
}