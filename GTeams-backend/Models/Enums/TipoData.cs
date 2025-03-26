using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.Models.Enums;

public enum TipoData
{
    [Display(Name = "Util")]
    Util,
    [Display(Name = "Fim de Semana")]
    FimDeSemana,
    [Display(Name = "Férias")]
    Ferias,
    [Display(Name = "Atestado")]
    Atestado,
    [Display(Name = "Meia Atuação")]
    MeiaAtuacao
}