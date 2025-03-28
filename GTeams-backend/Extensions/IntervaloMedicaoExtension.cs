using GTeams_backend.Dtos.DataPersonalizadaMedicaoDtos;
using GTeams_backend.Dtos.IntervaloMedicaoDtos;
using GTeams_backend.Models;

namespace GTeams_backend.Extensions;

public static class IntervaloMedicaoExtension
{
    public static RetornarIntervaloMedicaoDto ToReturnDto(this IntervaloMedicao intervaloMedicao)
    {
        return new RetornarIntervaloMedicaoDto
        {
            Id = intervaloMedicao.Id,
            DataInicial = intervaloMedicao.DataInicial,
            DataFinal = intervaloMedicao.DataFinal,
            DatasPersonalizadasMedicao = intervaloMedicao.DatasPersonalizadasMedicao
                .Select(d => new RetornarDataPersonalizadaMedicaoDto()
                {
                    Id = d.Id,
                    Data = d.Data,
                    TipoData = d.TipoData.ToString()
                }).ToList()
        };
    }
}