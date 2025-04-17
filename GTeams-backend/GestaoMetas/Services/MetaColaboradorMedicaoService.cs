using GTeams_backend.Data;
using GTeams_backend.GestaoMetas.Dtos.MetaColaboradorMedicaoDtos;
using GTeams_backend.GestaoMetas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoMetas.Services;

public class MetaColaboradorMedicaoService(AppDbContext appDbContext)
{
    public async Task<RetornarMetaColaboradorMedicaoDto> InserirAsync(InserirMetaColaboradorMedicaoDto inserirMetaColaboradorMedicaoDto)
    {
        
    }
    
    public async Task<MetaColaboradorMedicao?> ObterPorIdAsync(int id)
    {
        return await appDbContext.MetasColaboradoresMedicao
            .Include(mcm => mcm.Colaborador)
            .Include(mcm => mcm.IntervaloMedicao)
            .Include(mcm => mcm.Equipe)
            .FirstOrDefaultAsync(mcm => mcm.Id == id);
    }

    public async Task<MetaColaboradorMedicao?> ObterPorIdAsync(int colaboradorId, int intervaloMedicaoId, int equipeId)
    {
        return await appDbContext.MetasColaboradoresMedicao
            .Include(mcm => mcm.Colaborador)
            .Include(mcm => mcm.IntervaloMedicao)
            .Include(mcm => mcm.Equipe)
            .FirstOrDefaultAsync(mcm => mcm.ColaboradorId == colaboradorId && mcm.IntervaloMedicaoId == intervaloMedicaoId && mcm.EquipeId == equipeId);
    }
}