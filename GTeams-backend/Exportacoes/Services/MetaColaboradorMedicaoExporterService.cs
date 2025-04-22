using GTeams_backend.Exportacoes.Interfaces;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoMetas.Services;

namespace GTeams_backend.Exportacoes.Services;

public class MetaColaboradorMedicaoExporterService(MetaColaboradorMedicaoService metaColaboradorMedicaoService, IExporter<MetaColaboradorMedicao> exporter) 
{
    public async Task<byte[]> ExportMetaColaboradorMedicaoAsync()
    {
        List<MetaColaboradorMedicao> metas = await metaColaboradorMedicaoService.ObterTodosAsync();
        return exporter.ExportAsXlsx(metas);
    }
}