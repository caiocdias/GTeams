using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoPessoas.Dtos.ObservacaoDtos;

public class InserirObservacaoDto
{
    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(255, ErrorMessage = "A descrição não pode exceder 255 caracteres.")]
    public string Descricao { get; set; } = string.Empty;

    [Required(ErrorMessage = "O ID do colaborador é obrigatório.")]
    [Range(1, int.MaxValue, ErrorMessage = "O ID do colaborador deve ser um valor maior que zero.")]
    public int ColaboradorId { get; set; }

    [Required(ErrorMessage = "A data inicial é obrigatória.")]
    public DateOnly DataInicial { get; set; }

    [Required(ErrorMessage = "A data final é obrigatória.")]
    [CustomValidation(typeof(InserirObservacaoDto), nameof(ValidarDatas))]
    public DateOnly DataFinal { get; set; }

    public static ValidationResult? ValidarDatas(object? value, ValidationContext context)
    {
        if (context.ObjectInstance is InserirObservacaoDto dto)
        {
            return dto.DataFinal >= dto.DataInicial
                ? ValidationResult.Success
                : new ValidationResult("A data final deve ser maior ou igual à data inicial.");
        }

        return new ValidationResult("Erro ao validar as datas.");
    }
}