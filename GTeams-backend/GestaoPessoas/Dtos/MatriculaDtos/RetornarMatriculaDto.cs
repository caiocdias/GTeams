using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoPessoas.Dtos.MatriculaDtos;

public class RetornarMatriculaDto
{
    [Required]
    [StringLength(15)]
    public string Codigo { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string Descricao { get; set; } = string.Empty;
    
}