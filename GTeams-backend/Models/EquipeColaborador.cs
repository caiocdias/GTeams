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
    
    public bool? IsLider { get; set; }
    public DateOnly? DataEntrada { get; set; }
    public DateOnly? DataSaida { get; set; }
}