using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class Matricula
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(15)]
    public string Codigo { get; set; }
    
    [StringLength(50)]
    public string Descricao { get; set; }
    
    [Required]
    [ForeignKey("Colaborador")]
    public int ColaboradorId { get; set; }
    public Colaborador Colaborador { get; set; } = null!;
}