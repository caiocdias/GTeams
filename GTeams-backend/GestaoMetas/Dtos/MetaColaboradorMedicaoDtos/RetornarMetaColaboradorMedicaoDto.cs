namespace GTeams_backend.GestaoMetas.Dtos.MetaColaboradorMedicaoDtos;

public class RetornarMetaColaboradorMedicaoDto
{
    public int Id { get; set; }
    public int ColaboradorId { get; set; }
    public string ColaboradorNome { get; set; } = string.Empty;
    public int IntervaloMedicaoId { get; set; }
    public string IntervaloMedicaoNome { get; set; } = string.Empty;
    public int EquipeId { get; set; }
    public string EquipeNome { get; set; } = string.Empty;
    public decimal MetaNs { get; set; }
    public decimal MetaUs { get; set; }
    public int QtdDiasUteis { get; set; }
}