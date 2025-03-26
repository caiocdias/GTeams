using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class IntervaloMedicao
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
    
    public ICollection<DataPersonalizadaMedicao> DatasPersonalizadasMedicao { get; set; } = new List<DataPersonalizadaMedicao>();
    public ICollection<EquipeMetaMensal> EquipeMetaMensal { get; set; } = new List<EquipeMetaMensal>();
}