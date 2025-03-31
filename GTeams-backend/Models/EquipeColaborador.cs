using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class EquipeColaborador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Colaborador")]
    public int ColaboradorId { get; set; }
    public Colaborador Colaborador { get; set; } = null!;
    
    [Required]
    [ForeignKey("Equipe")]
    public int EquipeId { get; set; }
    public Equipe Equipe { get; set; } = null!;
    public bool IsLider { get; set; } = false;
    public bool Ativo { get; set; } = true;
}