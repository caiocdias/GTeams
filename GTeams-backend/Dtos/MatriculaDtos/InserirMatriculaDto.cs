using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.Dtos.MatriculaDtos;

public class InserirMatriculaDto
{
    [Required]
    [StringLength(50)]
    public string Descricao { get; set; } = string.Empty;
    
    [Required]
    [StringLength(15)]
    public string Codigo { get; set; } = string.Empty;
    
    public int ColaboradorId { get; set; }
}