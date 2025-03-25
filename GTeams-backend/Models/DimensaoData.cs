using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class DimensaoData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Nome { get; set; }
    
    [Required]
    public DateOnly DataInicial { get; set; }
    [Required]
    public DateOnly DataFinal { get; set; }
    
    [Required]
    public int QtdDiasUteis { get; set; }
    
    public ICollection<EquipeMetaMensal> EquipeMetaMensal { get; set; } = new List<EquipeMetaMensal>();
}