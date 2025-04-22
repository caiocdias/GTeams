namespace GTeams_backend.Exports.Interfaces;

public interface IExporter<T>
{
    byte[] ExportAsXlsx(IEnumerable<T> dados);
}