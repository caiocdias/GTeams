using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.Dtos.EquipeColaboradorDtos;

public class InserirEquipeColaboradorDto
{
    [Required]
    public int ColaboradorId { get; set; }
    
    [Required]
    public int EquipeId { get; set; }
    
    public bool IsLider { get; set; } = false;
    
    public bool Ativo { get; set; } = true;
}