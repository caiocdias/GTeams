using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTeams_backend.Models.Enums;

namespace GTeams_backend.Models;

public class DataPersonalizadaMedicao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    public DateTime Data { get; set; }
    [Required]
    public TipoData TipoData { get; set; }
    
    [Required]
    [ForeignKey("IntervaloMedicao")]
    public int IntervaloMedicaoId { get; set; }
    public IntervaloMedicao IntervaloMedicao { get; set; } = null!;
}