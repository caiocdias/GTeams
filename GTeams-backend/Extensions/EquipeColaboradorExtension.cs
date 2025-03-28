using GTeams_backend.Dtos.EquipeColaboradorDtos;
using GTeams_backend.Models;

namespace GTeams_backend.Extensions;

public static class EquipeColaboradorExtension
{
    public static RetornarEquipeColaboradorDto ToReturnDto(this EquipeColaborador ec)
    {
        return new RetornarEquipeColaboradorDto
        {
            Id = ec.Id,
            NomeColaborador = ec.Colaborador?.Nome ?? string.Empty,
            NomeEquipe = ec.Equipe?.Nome ?? string.Empty,
            IsLider = ec.IsLider,
            DataEntrada = ec.DataEntrada,
            DataSaida = ec.DataSaida
        };
    }
}