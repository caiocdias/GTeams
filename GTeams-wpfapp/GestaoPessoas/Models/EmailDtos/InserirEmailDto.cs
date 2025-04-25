using System.ComponentModel.DataAnnotations;

namespace GTeams_wpfapp.GestaoPessoas.Models.EmailDtos;

public class InserirEmailDto
{
    [Required(ErrorMessage = "A descrição do e-mail deve ser informada.")]
    [StringLength(50)]
    public string Descricao { get; set; } = string.Empty;
    
    [Required]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    public string Endereco { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Informe um Colaborador para associar ao E-mail.")]
    public int ColaboradorId { get; set; }
}