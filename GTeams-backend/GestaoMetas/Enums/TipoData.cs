using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoMetas.Enums;

public enum TipoData
{
    [Display(Name = "Útil")]
    Util,
    [Display(Name = "Fim de Semana")]
    FimDeSemana,
    [Display(Name = "Feriado")]
    Feriado,
    [Display(Name = "Férias")]
    Ferias,
    [Display(Name = "Atestado")]
    Atestado,
    [Display(Name = "Meia Atuação")]
    MeiaAtuacao
}