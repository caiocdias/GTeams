using GTeams_backend.Models;

namespace GTeams_backend.Dtos.EquipeMetaMensalDtos;

public class RetornarEquipeMetaMensalDto
{
    public int Id { get; set; }
    public int EquipeId { get; set; }
    public string EquipeNome { get; set; } = string.Empty;
    public int IntervaloMedicaoId { get; set; }
    public string IntervaloMedicaoNome { get; set; } = string.Empty;
    public decimal MetaMensalNs { get; set; }
    public decimal MetaMensalUs { get; set; }
}