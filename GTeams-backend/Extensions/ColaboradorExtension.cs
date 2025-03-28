using GTeams_backend.Dtos.ColaboradorDtos;
using GTeams_backend.Dtos.EmailDtos;
using GTeams_backend.Dtos.EquipeDtos;
using GTeams_backend.Dtos.MatriculaDtos;
using GTeams_backend.Models;

namespace GTeams_backend.Extensions;

public static class ColaboradorExtension
{
    public static RetornarColaboradorDto ToReturnDto(this Colaborador colaborador)
    {
        return new RetornarColaboradorDto
        {
            Id = colaborador.Id,
            Nome = colaborador.Nome,
            Cpf = colaborador.Cpf,
            Ativo = colaborador.Ativo,
            Funcao = colaborador.Funcao.ToString(),
            Emails = colaborador.Emails.Select(e => new RetornarEmailDto
            {
                Descricao = e.Descricao,
                Endereco = e.Endereco
            }).ToList(),
            Matriculas = colaborador.Matriculas.Select(m => new RetornarMatriculaDto
            {
                Codigo = m.Codigo,
                Descricao = m.Descricao
            }).ToList(),
            Equipes = colaborador.EquipesColaboradores.Select(ec => new RetornarEquipeDto
            {
                Id = ec.Equipe.Id,
                Nome = ec.Equipe.Nome,
                Ativo = ec.Equipe.Ativo
            }).ToList()
        };
    }
}