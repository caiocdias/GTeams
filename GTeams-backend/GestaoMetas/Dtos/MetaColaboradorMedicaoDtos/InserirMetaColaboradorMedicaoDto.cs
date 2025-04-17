using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoMetas.Dtos.MetaColaboradorMedicaoDtos;

public class InserirMetaColaboradorMedicaoDto
{
    [Required(ErrorMessage = "O ID do colaborador é obrigatório.")]
    public int ColaboradorId { get; set; }
    
    [Required(ErrorMessage = "O ID do intervalo de medição é obrigatório.")]
    public int IntervaloMedicaoId { get; set; }
    
    [Required(ErrorMessage = "O ID da equipe é obrigatório.")]
    public int EquipeId { get; set; }
    
    [Range(0, double.MaxValue, ErrorMessage = "A meta de NS deve ser um valor positivo.")]
    public decimal MetaNs { get; set; }
    [Range(0, double.MaxValue, ErrorMessage = "A meta de US deve ser um valor positivo.")]
    public decimal MetaUs { get; set; }

}