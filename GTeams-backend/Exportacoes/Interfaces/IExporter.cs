namespace GTeams_backend.Exportacoes.Interfaces;

public interface IExporter<T>
{
    byte[] ExportAsXlsx(IEnumerable<T> dados);
}