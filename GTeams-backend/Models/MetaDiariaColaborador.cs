using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class MetaDiariaColaborador
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [ForeignKey("Colaborador")]
    public int ColaboradorId { get; set; }
    public Colaborador Colaborador { get; set; }
    
    [Required]
    [ForeignKey("EquipeMetaMensal")]
    public int EquipeMetaMensalId { get; set; }
    public EquipeMetaMensal EquipeMetaMensal { get; set; }
    
    [Required]
    public DateOnly Data { get; set; }
    
    public decimal MetaDiariaNs { get; set; }
    public decimal MetaDiariaUs { get; set; }
}