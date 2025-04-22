using ClosedXML.Excel;
using GTeams_backend.Exports.Interfaces;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.Exports.Excel;

public class MetaColaboradorMedicaoExporter : IExporter<MetaColaboradorMedicao>
{
    public byte[] ExportAsXlsx(IEnumerable<MetaColaboradorMedicao> metaColaboradorMedicao)
    {
        using XLWorkbook workbook = new XLWorkbook();
        IXLWorksheet sheet = workbook.Worksheets.Add("Colaboradores");

        sheet.Cell("A1").Value = "ColaboradorNome";
        sheet.Cell("B1").Value = "EquipeNome";
        sheet.Cell("C1").Value = "IntervaloMedicao";
        sheet.Cell("D1").Value = "DataInicial";
        sheet.Cell("E1").Value = "DataFinal";
        sheet.Cell("F1").Value = "MetaNs";
        sheet.Cell("G1").Value = "MetaUs";
        sheet.Cell("H1").Value = "Chave";

        int row = 2;
        foreach (var item in metaColaboradorMedicao)
        {
            sheet.Cell("A" + row).Value = item.Colaborador.Nome;
            sheet.Cell("B" + row).Value = item.Equipe.Nome;
            sheet.Cell("C" + row).Value = item.IntervaloMedicao.Nome;
            sheet.Cell("D" + row).Value = item.IntervaloMedicao.DataInicial.ToString();
            sheet.Cell("E" + row).Value = item.IntervaloMedicao.DataFinal.ToString();
            sheet.Cell("F" + row).Value = item.MetaNs;
            sheet.Cell("G" + row).Value = item.MetaUs;
            sheet.Cell("H" + row).Value =
                (item.Colaborador.Nome + item.Equipe.Nome + item.IntervaloMedicao.Nome).Replace(" ", string.Empty);
            row++;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        return stream.ToArray();
    }
}