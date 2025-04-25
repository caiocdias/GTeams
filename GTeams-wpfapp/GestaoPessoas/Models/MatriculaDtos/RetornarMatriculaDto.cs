using System.ComponentModel.DataAnnotations;

namespace GTeams_wpfapp.GestaoPessoas.Models.MatriculaDtos;

public class RetornarMatriculaDto
{
    [Required]
    [StringLength(15)]
    public string Codigo { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string Descricao { get; set; } = string.Empty;
    
}