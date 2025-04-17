using GTeams_backend.GestaoMetas.Dtos.MetaColaboradorMedicaoDtos;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.GestaoMetas.Extensions;

public static class MetaColaboradorMedicaoExtension
{
    public static RetornarMetaColaboradorMedicaoDto ToReturnDto(this MetaColaboradorMedicao metaColaboradorMedicao)
    {
        return new RetornarMetaColaboradorMedicaoDto
        {
            Id = metaColaboradorMedicao.Id,
            ColaboradorId = metaColaboradorMedicao.ColaboradorId,
            ColaboradorNome = metaColaboradorMedicao.Colaborador.Nome,
            IntervaloMedicaoId = metaColaboradorMedicao.IntervaloMedicaoId,
            IntervaloMedicaoNome = metaColaboradorMedicao.IntervaloMedicao.Nome,
            EquipeId = metaColaboradorMedicao.EquipeId,
            EquipeNome = metaColaboradorMedicao.Equipe.Nome,
            MetaNs = metaColaboradorMedicao.MetaNs,
            MetaUs = metaColaboradorMedicao.MetaUs
        };
    }
}