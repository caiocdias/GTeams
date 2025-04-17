using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTeams_backend.GestaoMetas.Enums;
using GTeams_backend.GestaoPessoas.Models;

namespace GTeams_backend.GestaoMetas.Models;

public class DataPersonalizada
{
    [Key] public int Id { get; set; }

    [Required] public DateOnly Data { get; set; }
    [Required] public TipoData TipoData { get; set; }

    [Required]
    [ForeignKey("MetaColaboradorMedicao")]
    public int MetaColaboradorMedicaoId { get; set; }

    public MetaColaboradorMedicao MetaColaboradorMedicao { get; set; } = null!;
}