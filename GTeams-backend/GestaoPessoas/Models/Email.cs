using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.GestaoPessoas.Models;

public class Email
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Descricao { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress]
    [StringLength(50)]
    public string Endereco { get; set; } = string.Empty;
    
    [Required]
    [ForeignKey("Colaborador")]
    public int ColaboradorId { get; set; }
    public Colaborador Colaborador { get; set; } = null!;
}