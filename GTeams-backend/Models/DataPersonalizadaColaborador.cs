using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTeams_backend.Models.Enums;

namespace GTeams_backend.Models;

public class DataPersonalizadaColaborador
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateOnly Data { get; set; }

    [Required]
    public TipoData TipoData { get; set; }

    [Required]
    [ForeignKey("Colaborador")]
    public int ColaboradorId { get; set; }
    public Colaborador Colaborador { get; set; } = null!;
}