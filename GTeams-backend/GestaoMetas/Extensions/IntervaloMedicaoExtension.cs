using GTeams_backend.GestaoMetas.Dtos.IntervaloMedicaoDtos;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.GestaoMetas.Extensions;

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
        };
    }
}