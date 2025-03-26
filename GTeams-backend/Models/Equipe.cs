using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class Equipe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    public ICollection<Colaborador> Colaboradores { get; set; } = new List<Colaborador>();
    public ICollection<EquipeMetaMensal> EquipesMetasMensais { get; set; } = new List<EquipeMetaMensal>();
}