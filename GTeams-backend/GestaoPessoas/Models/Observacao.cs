using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.GestaoPessoas.Models;

public class Observacao
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(255)]
    public string Descricao { get; set; } = string.Empty;
    
    [Required]
    [ForeignKey("Colaborador")]
    public int ColaboradorId { get; set; }

    public Colaborador Colaborador { get; set; } = null!;
    
    public DateOnly DataInicial { get; set; }
    public DateOnly DataFinal { get; set; }
}