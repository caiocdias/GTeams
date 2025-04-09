using GTeams_backend.GestaoPessoas.Dtos.ObservacaoDtos;
using GTeams_backend.GestaoPessoas.Models;

namespace GTeams_backend.GestaoPessoas.Extensions;

public static class ObservacaoExtension
{
    public static RetornarObservacaoDto ToReturnDto(this Observacao observacao)
    {
        return new RetornarObservacaoDto
        {
            Id = observacao.Id,
            Descricao = observacao.Descricao,
            ColaboradorId = observacao.ColaboradorId,
            ColaboradorNome = observacao.Colaborador.Nome,
            DataInicial = observacao.DataInicial,
            DataFinal = observacao.DataFinal
        };
    }
}