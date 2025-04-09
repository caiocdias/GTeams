namespace GTeams_backend.GestaoMetas.Dtos.EquipeMetaMensalDtos;

public class InserirEquipeMetaMensalDto
{
    public int EquipeId { get; set; }
    public int IntervaloMedicaoId { get; set; }
    public decimal MetaMensalNs { get; set; }
    public decimal MetaMensalUs { get; set; }
}