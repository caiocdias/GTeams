namespace GTeams_backend.GestaoMetas.Dtos.IntervaloMedicaoDtos;

public class RetornarIntervaloMedicaoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public DateOnly DataInicial { get; set; }
    public DateOnly DataFinal { get; set; }
}