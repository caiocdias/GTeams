using GTeams_backend.GestaoMetas.Dtos.EquipeMetaMensalDtos;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.GestaoMetas.Extensions;

public static class EquipeMetaMensalExtension
{
    public static RetornarEquipeMetaMensalDto ToReturnDto(this EquipeMetaMensal equipeMetaMensal)
    {
        return new RetornarEquipeMetaMensalDto
        {
            Id = equipeMetaMensal.Id,
            EquipeId = equipeMetaMensal.EquipeId,
            EquipeNome = equipeMetaMensal.Equipe.Nome,
            IntervaloMedicaoId = equipeMetaMensal.IntervaloMedicaoId,
            IntervaloMedicaoNome = equipeMetaMensal.IntervaloMedicao.Nome,
            MetaMensalNs = equipeMetaMensal.MetaMensalNs,
            MetaMensalUs = equipeMetaMensal.MetaMensalUs
        };
    }
}