using GTeams_backend.Dtos.EquipeDtos;
using GTeams_backend.GestaoPessoas.Models;

namespace GTeams_backend.Extensions;

public static class EquipeExtension
{
    public static RetornarEquipeDto ToReturnDto(this Equipe equipe)
    {
        return new RetornarEquipeDto
        {
            Id = equipe.Id,
            Nome = equipe.Nome,
            Ativo = equipe.Ativo
        };
    }
}