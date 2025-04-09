using System.ComponentModel.DataAnnotations;
using GTeams_backend.GestaoPessoas.Dtos.EmailDtos;
using GTeams_backend.GestaoPessoas.Dtos.MatriculaDtos;
using GTeams_backend.GestaoPessoas.Enums;

namespace GTeams_backend.GestaoPessoas.Dtos.ColaboradorDtos;

public class InserirColaboradorDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres.")]
    public string Nome { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "O nome de usuário é obrigatório.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "O nome de usuário deve ter entre 3 e 50 caracteres.")]
    [RegularExpression(@"^[a-zA-Z0-9._-]{3,50}$", ErrorMessage = "O nome de usuário deve conter apenas letras, números, pontos, hífens ou underscores.")]
    public string User { get; set; } = string.Empty;

    [Required(ErrorMessage = "O CPF é obrigatório.")]
    [StringLength(14, MinimumLength = 14, ErrorMessage = "O CPF deve ter exatamente 14 caracteres no formato 000.000.000-00.")]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato 000.000.000-00.")]
    public string Cpf { get; set; } = "000.000.000-00";

    [Required(ErrorMessage = "A função é obrigatória.")]
    [EnumDataType(typeof(Funcao), ErrorMessage = "A função informada não é válida.")]
    public Funcao Funcao { get; set; }

    public bool Ativo { get; set; } = true;

    public ICollection<InserirMatriculaDto> Matriculas { get; set; } = new List<InserirMatriculaDto>();
    public ICollection<InserirEmailDto> Emails { get; set; } = new List<InserirEmailDto>();

    [Required(ErrorMessage = "A senha é obrigatória.")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "A senha deve ter entre 8 e 100 caracteres.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,100}$", 
        ErrorMessage = "A senha deve conter pelo menos uma letra maiúscula, uma minúscula, um número e um caractere especial.")]
    public string Password { get; set; } = string.Empty;
}