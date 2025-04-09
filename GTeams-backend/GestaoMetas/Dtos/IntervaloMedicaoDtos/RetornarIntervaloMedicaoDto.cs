using System.ComponentModel.DataAnnotations;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.GestaoMetas.Dtos.IntervaloMedicaoDtos;

public class RetornarIntervaloMedicaoDto
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    [Required]
    public DateOnly DataInicial { get; set; }
    [Required]
    public DateOnly DataFinal { get; set; }
    
    public ICollection<DataPersonalizada> DatasPersonalizadasMedicao { get; set; } = new List<DataPersonalizada>();
}