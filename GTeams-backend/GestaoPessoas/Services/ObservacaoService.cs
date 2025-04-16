using GTeams_backend.Data;
using GTeams_backend.GestaoPessoas.Dtos.ObservacaoDtos;
using GTeams_backend.GestaoPessoas.Extensions;
using GTeams_backend.GestaoPessoas.Models;

namespace GTeams_backend.GestaoPessoas.Services;

public class ObservacaoService(AppDbContext appDbContext, ColaboradorService colaboradorService)
{
    public async Task<RetornarObservacaoDto> InserirObservacaoAsync(InserirObservacaoDto inserirObservacaoDto)
    {
        Colaborador? colaborador = await colaboradorService.ObterColaboradorPorIdAsync(inserirObservacaoDto.ColaboradorId);
        if (colaborador == null)
            throw new NullReferenceException("O colaborador indicado não existe.");

        Observacao observacao = new Observacao()
        {
            Descricao = inserirObservacaoDto.Descricao,
            ColaboradorId = inserirObservacaoDto.ColaboradorId,
            DataInicial = inserirObservacaoDto.DataInicial,
            DataFinal = inserirObservacaoDto.DataFinal
        };
        
        appDbContext.Observacoes.Add(observacao);
        await appDbContext.SaveChangesAsync();

        return observacao.ToReturnDto();
    }
}