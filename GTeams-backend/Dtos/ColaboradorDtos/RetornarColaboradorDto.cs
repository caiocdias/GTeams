using System.ComponentModel.DataAnnotations;
using GTeams_backend.Dtos.EmailDtos;
using GTeams_backend.Dtos.EquipeDtos;
using GTeams_backend.Dtos.MatriculaDtos;

namespace GTeams_backend.Dtos.ColaboradorDtos;

public class RetornarColaboradorDto
{
    public int Id { get; set; }

    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [StringLength(14)]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato 000.000.000-00.")]
    public string Cpf { get; set; } = "000.000.000-00";

    public bool Ativo { get; set; } = true;

    [Required]
    public string Funcao { get; set; } = string.Empty;

    public ICollection<RetornarMatriculaDto> Matriculas { get; set; } = new List<RetornarMatriculaDto>();
    public ICollection<RetornarEmailDto> Emails { get; set; } = new List<RetornarEmailDto>();
    public ICollection<RetornarEquipeDto> Equipes { get; set; } = new List<RetornarEquipeDto>();

}