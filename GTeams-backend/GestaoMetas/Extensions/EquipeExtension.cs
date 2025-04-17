using GTeams_backend.GestaoMetas.Dtos.EquipeDtos;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.GestaoMetas.Extensions;

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