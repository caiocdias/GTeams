using GTeams_backend.Exports.Interfaces;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoMetas.Services;

namespace GTeams_backend.Exports.Services;

public class MetaColaboradorMedicaoExporterService(
    MetaColaboradorMedicaoService metaColaboradorMedicaoService,
    IExporter<MetaColaboradorMedicao> exporter)
{
    public async Task<byte[]> ExportMetaColaboradorMedicaoAsync()
    {
        List<MetaColaboradorMedicao> metas = await metaColaboradorMedicaoService.ObterTodosAsync();
        return exporter.ExportAsXlsx(metas);
    }

    public async Task<byte[]> ExportMetaColaboradorMedicaoDailyAsync()
    {
        List<MetaColaboradorMedicao> metas = await metaColaboradorMedicaoService.ObterTodosAsync();
        return exporter.ExportDailyAsXlsx(metas);
    }
}