using System.ComponentModel.DataAnnotations;
using GTeams_backend.Dtos.EmailDtos;
using GTeams_backend.Dtos.MatriculaDtos;
using GTeams_backend.Models.Enums;

namespace GTeams_backend.Dtos.ColaboradorDtos;

public class InserirColaboradorDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(14)]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato 000.000.000-00.")]
    public string Cpf { get; set; } = "000.000.000-00";

    [Required(ErrorMessage = "A função é obrigatória.")]
    public Funcao Funcao { get; set; }

    public ICollection<InserirMatriculaDto> Matriculas { get; set; } = new List<InserirMatriculaDto>();
    public ICollection<InserirEmailDto> Emails { get; set; } = new List<InserirEmailDto>();

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Password { get; set; } = string.Empty;
}