using GTeams_backend.GestaoMetas.Dtos.ColaboradorEquipeMetaMensalDtos;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.GestaoMetas.Extensions;

public static class ColaboradorEquipeMetaMensalExtension
{
    
    public static RetornarColaboradorEquipeMetaMensalDto ToReturnDto(this ColaboradorEquipeMetaMensal colaboradorEquipeMetaMensal)
    {
        return new RetornarColaboradorEquipeMetaMensalDto
        {
            Id = colaboradorEquipeMetaMensal.Id,
            ColaboradorId = colaboradorEquipeMetaMensal.ColaboradorId,
            NomeColaborador = colaboradorEquipeMetaMensal.Colaborador?.Nome ?? string.Empty,
            EquipeMetaMensalId = colaboradorEquipeMetaMensal.EquipeMetaMensalId,
            NomeEquipeMetaMensal = colaboradorEquipeMetaMensal.EquipeMetaMensal?.Equipe?.Nome ?? string.Empty,
            Ativo = colaboradorEquipeMetaMensal.Ativo
        };
    }
}