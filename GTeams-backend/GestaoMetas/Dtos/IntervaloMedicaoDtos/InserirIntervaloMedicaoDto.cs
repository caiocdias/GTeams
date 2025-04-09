using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoMetas.Dtos.IntervaloMedicaoDtos;

public class InserirIntervaloMedicaoDto
{
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    public DateOnly DataInicial { get; set; }
    [Required]
    public DateOnly DataFinal { get; set; }
}