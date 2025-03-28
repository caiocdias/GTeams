using System.ComponentModel.DataAnnotations;
using GTeams_backend.Dtos.DataPersonalizadaMedicaoDtos;

namespace GTeams_backend.Dtos.IntervaloMedicaoDtos;

public class RetornarIntervaloMedicaoDto
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    public DateOnly DataInicial { get; set; }
    [Required]
    public DateOnly DataFinal { get; set; }
    
    public ICollection<RetornarDataPersonalizadaMedicaoDto> DatasPersonalizadasMedicao { get; set; } = new List<RetornarDataPersonalizadaMedicaoDto>();
}