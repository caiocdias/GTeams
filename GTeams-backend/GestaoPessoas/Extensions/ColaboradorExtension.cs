using GTeams_backend.GestaoPessoas.Dtos.ColaboradorDtos;
using GTeams_backend.GestaoPessoas.Dtos.EmailDtos;
using GTeams_backend.GestaoPessoas.Dtos.MatriculaDtos;
using GTeams_backend.GestaoPessoas.Models;

namespace GTeams_backend.GestaoPessoas.Extensions;

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
            }).ToList()
        };
    }
}