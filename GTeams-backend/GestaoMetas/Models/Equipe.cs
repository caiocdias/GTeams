using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.GestaoMetas.Models;

public class Equipe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    public bool Ativo { get; set; } = true;
}