using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class Equipe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public static int Id { get; set; }
    
    [StringLength(100)]
    public string Nome { get; set; }
    
    public ICollection<Colaborador> Colaborador { get; set; } = new HashSet<Colaborador>();
    public ICollection<EquipeMetaMensal> EquipeMetaMensal { get; set; } = new List<EquipeMetaMensal>();
}