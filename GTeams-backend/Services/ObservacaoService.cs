using GTeams_backend.Data;
using GTeams_backend.Dtos.ObservacaoDtos;
using GTeams_backend.Extensions;
using GTeams_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.Services;

public class ObservacaoService(AppDbContext appDbContext, ColaboradorService colaboradorService)
{
    public async Task<RetornarObservacaoDto> InserirObservacaoAsync(InserirObservacaoDto inserirObservacaoDto)
    {
        Colaborador? colaborador = await colaboradorService.ObterColaboradorPorIdAsync(inserirObservacaoDto.ColaboradorId);
        if (colaborador is not { Ativo: true })
            throw new InvalidOperationException("O colaborador não foi encontrado.");

        if (inserirObservacaoDto.DataInicial > inserirObservacaoDto.DataFinal)
            throw new InvalidOperationException("A data inicial deve ser menor ou igual à data final.");
        
        Observacao observacao = new Observacao
        {
            Descricao = inserirObservacaoDto.Descricao,
            Colaborador = colaborador,
            DataInicial = inserirObservacaoDto.DataInicial,
            DataFinal = inserirObservacaoDto.DataFinal
        };
        appDbContext.Observacoes.Add(observacao);
        await appDbContext.SaveChangesAsync();
        
        return observacao.ToReturnDto();
    }

    public async Task<Observacao?> ObterObservacaoPorIdAsync(int id)
    {
        return await appDbContext.Observacoes
            .Include(o => o.Colaborador)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<List<Observacao>> ObterTodasObservacoesAsync()
    {
        return await appDbContext.Observacoes
            .Include(o => o.Colaborador)
            .ToListAsync();
    }

    public async Task<RetornarObservacaoDto> DeletarObservacaoAsync(int id)
    {
        Observacao? observacao = await ObterObservacaoPorIdAsync(id);
        if (observacao is null)
            throw new InvalidOperationException("Observação não foi encontrada.");

        appDbContext.Observacoes.Remove(observacao);
        await appDbContext.SaveChangesAsync();
        
        return observacao.ToReturnDto();
    }
}