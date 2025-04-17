using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoMetas.Dtos.EquipeDtos;

public class InserirEquipeDto
{
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;
}