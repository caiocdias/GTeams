using GTeams_backend.Data;
using GTeams_backend.GestaoMetas.Dtos.IntervaloMedicaoDtos;
using GTeams_backend.GestaoMetas.Extensions;
using GTeams_backend.GestaoMetas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoMetas.Services;

public class IntervaloMedicaoService(AppDbContext appDbContext)
{
    public async Task<RetornarIntervaloMedicaoDto> InserirAsync(InserirIntervaloMedicaoDto inserirIntervaloMedicaoDto)
    {
        IntervaloMedicao? buscaIntervaloMedicao = await ObterPorNomeAsync(inserirIntervaloMedicaoDto.Nome);
        if (buscaIntervaloMedicao != null)
            throw new InvalidOperationException("O intervalo de medição já está cadastrada.");

        IntervaloMedicao novoIntervaloMedicao = new IntervaloMedicao
        {
            Nome = inserirIntervaloMedicaoDto.Nome,
            DataInicial = inserirIntervaloMedicaoDto.DataInicial,
            DataFinal = inserirIntervaloMedicaoDto.DataFinal
        };
        
        appDbContext.IntervalosMedicao.Add(novoIntervaloMedicao);
        await appDbContext.SaveChangesAsync();

        return novoIntervaloMedicao.ToReturnDto();
    }

    public async Task<IntervaloMedicao?> ObterPorIdAsync(int id)
    {
        return await appDbContext.IntervalosMedicao
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<IntervaloMedicao?> ObterPorNomeAsync(string nome)
    {
        return await appDbContext.IntervalosMedicao
            .FirstOrDefaultAsync(i => i.Nome.ToLower() == nome.ToLower());
    }
    public async Task<List<IntervaloMedicao>> ObterTodosIntervalosMedicao()
    {
        return await appDbContext.IntervalosMedicao
            .ToListAsync();
    }

    public async Task<bool> DeletarAsync(int id)
    {
        IntervaloMedicao? intervaloMedicao = await ObterPorIdAsync(id);
        if (intervaloMedicao == null)
            throw new KeyNotFoundException("Intervalo de medição não encontrado.");

        appDbContext.IntervalosMedicao.Remove(intervaloMedicao);
        await appDbContext.SaveChangesAsync();
        return true;
    }
}