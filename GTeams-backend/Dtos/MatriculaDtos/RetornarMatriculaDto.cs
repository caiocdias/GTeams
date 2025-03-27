using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.Dtos.MatriculaDtos;

public class RetornarMatriculaDto
{
    [Required]
    [StringLength(15)]
    public string Codigo { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string Descricao { get; set; } = string.Empty;
    
}