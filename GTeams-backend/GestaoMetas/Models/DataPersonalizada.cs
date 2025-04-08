using System.ComponentModel.DataAnnotations;
using GTeams_backend.GestaoMetas.Enums;
using GTeams_backend.GestaoPessoas.Models;

namespace GTeams_backend.GestaoMetas.Models;

public class DataPersonalizada
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateOnly Data { get; set; }

    [Required]
    public TipoData TipoData { get; set; }

    public int? ColaboradorId { get; set; }
    public Colaborador? Colaborador { get; set; }

    public int? IntervaloMedicaoId { get; set; }
    public IntervaloMedicao? IntervaloMedicao { get; set; }
}