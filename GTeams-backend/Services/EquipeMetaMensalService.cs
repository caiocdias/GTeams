using GTeams_backend.Data;
using GTeams_backend.Dtos.EquipeMetaMensalDtos;
using GTeams_backend.Extensions;
using GTeams_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.Services;

public class EquipeMetaMensalService(AppDbContext appDbContext, EquipeService equipeService, IntervaloMedicaoService intervaloMedicaoService)
{
    public async Task<RetornarEquipeMetaMensalDto> InserirEquipeMetaMensalAsync(InserirEquipeMetaMensalDto inserirEquipeMetaMensalDto)
    {
        Equipe? equipe = await equipeService.ObterEquipePorIdAsync(inserirEquipeMetaMensalDto.EquipeId);
        if (equipe is not { Ativo: true })
            throw new InvalidOperationException("Equipe não encontrada ou está inativa.");

        IntervaloMedicao? intervaloMedicao = await intervaloMedicaoService.ObterIntervaloMedicaoPorId(inserirEquipeMetaMensalDto.IntervaloMedicaoId);
        if (intervaloMedicao == null)
            throw new InvalidOperationException("Intervalo de medição não encontrado.");

        EquipeMetaMensal? equipeMetaExistente = await ObterEquipeMetaMensalPorIdAsync(inserirEquipeMetaMensalDto.EquipeId, inserirEquipeMetaMensalDto.IntervaloMedicaoId);

        if (equipeMetaExistente != null)
            throw new InvalidOperationException("Meta mensal para essa equipe e intervalo já cadastrada.");

        EquipeMetaMensal novaMetaMensal = new EquipeMetaMensal
        {
            EquipeId = inserirEquipeMetaMensalDto.EquipeId,
            IntervaloMedicaoId = inserirEquipeMetaMensalDto.IntervaloMedicaoId,
            MetaMensalNs = inserirEquipeMetaMensalDto.MetaMensalNs,
            MetaMensalUs = inserirEquipeMetaMensalDto.MetaMensalUs
        };

        appDbContext.EquipesMetasMensais.Add(novaMetaMensal);
        await appDbContext.SaveChangesAsync();

        return novaMetaMensal.ToReturnDto();
    }

    public async Task<EquipeMetaMensal?> ObterEquipeMetaMensalPorIdAsync(int equipeId, int intervaloMedicaoId)
    {
        return await appDbContext.EquipesMetasMensais
            .Include(emm => emm.Equipe)
            .Include(emm => emm.IntervaloMedicao)
            .FirstOrDefaultAsync(emm => emm.EquipeId == equipeId && emm.IntervaloMedicaoId == intervaloMedicaoId);
    }

    public async Task<List<EquipeMetaMensal>> ObterTodosEquipeMetaMensalAsync()
    {
        return await appDbContext.EquipesMetasMensais
            .Include(emm => emm.Equipe)
            .Include(emm => emm.IntervaloMedicao)
            .ToListAsync();
    }

    public async Task<RetornarEquipeMetaMensalDto> DeletarEquipeMetaMensalAsync(int equipeId, int intervaloMedicaoId)
    {
        EquipeMetaMensal? equipeMetaMensal = await ObterEquipeMetaMensalPorIdAsync(equipeId, intervaloMedicaoId);
        if (equipeMetaMensal is null)
            throw new InvalidOperationException("Vinculo entre Equipe e Intervalo de medição não encontrado.");
        
        appDbContext.EquipesMetasMensais.Remove(equipeMetaMensal);
        await appDbContext.SaveChangesAsync();
        
        return equipeMetaMensal.ToReturnDto();
    }
}