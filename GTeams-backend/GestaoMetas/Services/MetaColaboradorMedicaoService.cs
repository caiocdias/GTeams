using GTeams_backend.Data;
using GTeams_backend.GestaoMetas.Dtos.MetaColaboradorMedicaoDtos;
using GTeams_backend.GestaoMetas.Extensions;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoPessoas.Models;
using GTeams_backend.GestaoPessoas.Services;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoMetas.Services;

public class MetaColaboradorMedicaoService(
    AppDbContext appDbContext,
    ColaboradorService colaboradorService,
    EquipeService equipeService,
    IntervaloMedicaoService intervaloMedicaoService)
{
    public async Task<RetornarMetaColaboradorMedicaoDto> InserirAsync(
        InserirMetaColaboradorMedicaoDto inserirMetaColaboradorMedicaoDto)
    {
        MetaColaboradorMedicao? metaColaboradorMedicao = await ObterPorIdAsync(
            colaboradorId: inserirMetaColaboradorMedicaoDto.ColaboradorId,
            intervaloMedicaoId: inserirMetaColaboradorMedicaoDto.IntervaloMedicaoId, equipeId:
            inserirMetaColaboradorMedicaoDto.EquipeId);
        if (metaColaboradorMedicao is not null)
            throw new InvalidOperationException(
                "O colaborador desta equipe já está cadastrado no intervalo de medição informado.");

        Colaborador? colaborador =
            await colaboradorService.ObterPorIdAsync(inserirMetaColaboradorMedicaoDto.ColaboradorId);
        if (colaborador is not { Ativo: true })
            throw new InvalidOperationException("O colaborador informado não existe.");


        Equipe? equipe = await equipeService.ObterPorIdAsync(inserirMetaColaboradorMedicaoDto.EquipeId);
        if (equipe is not { Ativo: true })
            throw new InvalidOperationException("A equipe informada não existe.");

        IntervaloMedicao? intervaloMedicao =
            await intervaloMedicaoService.ObterPorIdAsync(inserirMetaColaboradorMedicaoDto.IntervaloMedicaoId);
        if (intervaloMedicao is null)
            throw new InvalidOperationException("O intervalo de medição informado não existe.");

        MetaColaboradorMedicao novaMetaColaboradorMedicao = new MetaColaboradorMedicao
        {
            ColaboradorId = inserirMetaColaboradorMedicaoDto.ColaboradorId,
            IntervaloMedicaoId = inserirMetaColaboradorMedicaoDto.IntervaloMedicaoId,
            EquipeId = inserirMetaColaboradorMedicaoDto.EquipeId,
            MetaNs = inserirMetaColaboradorMedicaoDto.MetaNs,
            MetaUs = inserirMetaColaboradorMedicaoDto.MetaUs
        };

        appDbContext.MetasColaboradoresMedicao.Add(novaMetaColaboradorMedicao);
        await appDbContext.SaveChangesAsync();

        MetaColaboradorMedicao? meta = await ObterPorIdAsync(novaMetaColaboradorMedicao.Id);
        if (meta == null)
            throw new InvalidOperationException("Houve um erro ao inserir o item.");

        meta.GerarDatas();
        meta.GerarQtdDiasUteis();

        await appDbContext.SaveChangesAsync();
        return meta.ToReturnDto();
    }

    public async Task<MetaColaboradorMedicao?> ObterPorIdAsync(int id)
    {
        return await appDbContext.MetasColaboradoresMedicao
            .Include(mcm => mcm.Colaborador)
            .ThenInclude(c => c.Matriculas)
            .Include(mcm => mcm.IntervaloMedicao)
            .Include(mcm => mcm.Equipe)
            .Include(mcm => mcm.DatasPersonalizadasMedicao)
            .FirstOrDefaultAsync(mcm => mcm.Id == id);
    }

    public async Task<List<MetaColaboradorMedicao>> ObterTodosAsync()
    {
        return await appDbContext.MetasColaboradoresMedicao
            .Include(mcm => mcm.Colaborador)
            .ThenInclude(c => c.Matriculas)
            .Include(mcm => mcm.IntervaloMedicao)
            .Include(mcm => mcm.Equipe)
            .Include(mcm => mcm.DatasPersonalizadasMedicao)
            .ToListAsync();
    }

    public async Task<MetaColaboradorMedicao?> ObterPorIdAsync(int colaboradorId, int intervaloMedicaoId, int equipeId)
    {
        return await appDbContext.MetasColaboradoresMedicao
            .Include(mcm => mcm.Colaborador)
            .ThenInclude(c => c.Matriculas)
            .Include(mcm => mcm.IntervaloMedicao)
            .Include(mcm => mcm.Equipe)
            .Include(mcm => mcm.DatasPersonalizadasMedicao)
            .FirstOrDefaultAsync(mcm =>
                mcm.ColaboradorId == colaboradorId && mcm.IntervaloMedicaoId == intervaloMedicaoId &&
                mcm.EquipeId == equipeId);
    }

    public async Task<RetornarMetaColaboradorMedicaoDto> DeletarAsync(int metaColaboradorMedicaoId)
    {
        MetaColaboradorMedicao? metaColaboradorMedicao = await ObterPorIdAsync(metaColaboradorMedicaoId);
        if (metaColaboradorMedicao is null)
            throw new InvalidOperationException("O intervalo de medição informado não existe.");

        appDbContext.MetasColaboradoresMedicao.Remove(metaColaboradorMedicao);
        await appDbContext.SaveChangesAsync();

        return metaColaboradorMedicao.ToReturnDto();
    }
}