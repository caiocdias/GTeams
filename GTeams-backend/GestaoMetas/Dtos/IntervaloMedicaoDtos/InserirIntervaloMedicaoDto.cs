using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoMetas.Dtos.IntervaloMedicaoDtos;

public class InserirIntervaloMedicaoDto
{
    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, MinimumLength = 5)]
    public string Nome { get; set; } = string.Empty;

    [Required(ErrorMessage = "A data inicial é obrigatória.")]
    public DateOnly DataInicial { get; set; }

    [Required(ErrorMessage = "A data final é obrigatória.")]
    [CustomValidation(typeof(InserirIntervaloMedicaoDto), nameof(ValidarDatas))]
    public DateOnly DataFinal { get; set; }

    public static ValidationResult? ValidarDatas(object? value, ValidationContext context)
    {
        if (context.ObjectInstance is InserirIntervaloMedicaoDto dto)
        {
            return dto.DataFinal >= dto.DataInicial
                ? ValidationResult.Success
                : new ValidationResult("A data final não pode ser anterior à data inicial.");
        }

        return new ValidationResult("Dados inválidos para validação.");
    }
}