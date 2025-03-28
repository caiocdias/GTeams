namespace GTeams_backend.Dtos.DataPersonalizadaMedicaoDtos;

public class RetornarDataPersonalizadaMedicaoDto
{
    public int Id { get; set; }
    public DateOnly Data { get; set; }
    public string TipoData { get; set; } = string.Empty;
}