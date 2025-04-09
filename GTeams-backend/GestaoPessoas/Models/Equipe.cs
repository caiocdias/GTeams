using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTeams_backend.GestaoMetas.Models;

namespace GTeams_backend.GestaoPessoas.Models;

public class Equipe
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
    
    public bool Ativo { get; set; } = true;
    public ICollection<EquipeMetaMensal> EquipesMetasMensais { get; set; } = new List<EquipeMetaMensal>();
}