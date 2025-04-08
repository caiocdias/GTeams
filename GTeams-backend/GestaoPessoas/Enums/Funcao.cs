using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoPessoas.Enums;

public enum Funcao
{
    [Display(Name = "Operador")]
    Operador,
    [Display(Name = "Administrador")]
    Administrador,
    [Display(Name = "Visualizador")]
    Visualizador,
    [Display(Name = "Supervisor")]
    Supervisor
}