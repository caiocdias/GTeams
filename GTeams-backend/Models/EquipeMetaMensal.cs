using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class EquipeMetaMensal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Equipe")]
    public int EquipeId { get; set; }
    public Equipe Equipe { get; set; } = null!;

    [Required]
    [ForeignKey("IntervaloMedicao")]
    public int IntervaloMedicaoId { get; set; }
    public IntervaloMedicao IntervaloMedicao { get; set; } = null!;
    
    [Required]
    public decimal MetaMensalNs { get; set; }
    
    [Required]
    public decimal MetaMensalUs { get; set; }
}