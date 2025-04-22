using ClosedXML.Excel;
using GTeams_backend.Exports.Interfaces;
using GTeams_backend.GestaoMetas.Enums;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.Exports.Excel;

public class MetaColaboradorMedicaoExporter : IExporter<MetaColaboradorMedicao>
{
    public byte[] ExportAsXlsx(IEnumerable<MetaColaboradorMedicao> metaColaboradorMedicao)
    {
        using XLWorkbook workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.Worksheets.Add("Colaboradores");

        sheet.Cell("A1").Value = "ColaboradorNome";
        sheet.Cell("B1").Value = "MatriculaCodigo";
        sheet.Cell("C1").Value = "EquipeNome";
        sheet.Cell("D1").Value = "IntervaloMedicao";
        sheet.Cell("E1").Value = "DataInicial";
        sheet.Cell("F1").Value = "DataFinal";
        sheet.Cell("G1").Value = "MetaNs";
        sheet.Cell("H1").Value = "MetaUs";
        sheet.Cell("I1").Value = "Chave";

        int row = 2;
        foreach (var item in metaColaboradorMedicao)
        {
            sheet.Cell("A" + row).Value = item.Colaborador.Nome;
            sheet.Cell("B" + row).Value = item.Colaborador.Matriculas.FirstOrDefault()?.Codigo ?? string.Empty;
            sheet.Cell("C" + row).Value = item.Equipe.Nome;
            sheet.Cell("D" + row).Value = item.IntervaloMedicao.Nome;
            sheet.Cell("E" + row).Value = item.IntervaloMedicao.DataInicial.ToString();
            sheet.Cell("F" + row).Value = item.IntervaloMedicao.DataFinal.ToString();
            sheet.Cell("G" + row).Value = item.MetaNs;
            sheet.Cell("H" + row).Value = item.MetaUs;
            sheet.Cell("I" + row).Value =
                (item.Colaborador.Nome + item.Equipe.Nome + item.IntervaloMedicao.Nome).Replace(" ", string.Empty);
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }

    public byte[] ExportDailyAsXlsx(IEnumerable<MetaColaboradorMedicao> metaColaboradorMedicao)
    {
        using XLWorkbook workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.Worksheets.Add("Colaboradores");

        sheet.Cell("A1").Value = "ColaboradorNome";
        sheet.Cell("B1").Value = "MatriculaCodigo";
        sheet.Cell("C1").Value = "EquipeNome";
        sheet.Cell("D1").Value = "IntervaloMedicao";
        sheet.Cell("E1").Value = "Data";
        sheet.Cell("F1").Value = "TipoData";
        sheet.Cell("G1").Value = "MetaNsDiaria";
        sheet.Cell("H1").Value = "MetaUsDiaria";
        sheet.Cell("I1").Value = "Chave";

        int row = 2;

        foreach (var item in metaColaboradorMedicao)
        {
            foreach (DataPersonalizada data in item.DatasPersonalizadasMedicao)
            {
                sheet.Cell("A" + row).Value = item.Colaborador.Nome;
                sheet.Cell("B" + row).Value = item.Colaborador.Matriculas.FirstOrDefault()?.Codigo ?? string.Empty;
                sheet.Cell("C" + row).Value = item.Equipe.Nome;
                sheet.Cell("D" + row).Value = item.IntervaloMedicao.Nome;
                sheet.Cell("E" + row).Value = data.Data.ToString();
                sheet.Cell("F" + row).Value = data.TipoData.GetDisplayName();

                if (data.TipoData == TipoData.Util)
                {
                    sheet.Cell("G" + row).Value = item.MetaNs / item.QtdDiasUteis;
                    sheet.Cell("H" + row).Value = item.MetaUs / item.QtdDiasUteis;
                }
                else if (data.TipoData == TipoData.MeiaAtuacao)
                {
                    sheet.Cell("G" + row).Value = item.MetaNs / item.QtdDiasUteis / 2;
                    sheet.Cell("H" + row).Value = item.MetaUs / item.QtdDiasUteis / 2;
                }
                else
                {
                    sheet.Cell("G" + row).Value = 0;
                    sheet.Cell("H" + row).Value = 0;
                }

                sheet.Cell("I" + row).Value =
                    (item.Colaborador.Nome + item.Equipe.Nome + item.IntervaloMedicao.Nome).Replace(" ", string.Empty);
                row++;
            }
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}