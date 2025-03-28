using System.ComponentModel.DataAnnotations;
using GTeams_backend.Models;

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
    
    public ICollection<DataPersonalizadaMedicao> DatasPersonalizadasMedicao { get; set; } = new List<DataPersonalizadaMedicao>();
}