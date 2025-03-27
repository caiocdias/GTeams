using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.Dtos.MatriculaDtos;

public class InserirMatriculaDto
{
    [Required]
    [StringLength(15)]
    public string Codigo { get; set; } = string.Empty;
    
    [Required]
    [StringLength(50)]
    public string Descricao { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Informe um Colaborador para associar à Matrícula.")]
    public int ColaboradorId { get; set; }
}