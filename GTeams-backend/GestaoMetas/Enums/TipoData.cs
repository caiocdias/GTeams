using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GTeams_backend.GestaoMetas.Enums;

public enum TipoData
{
    [Display(Name = "Útil")] Util,
    [Display(Name = "Fim de Semana")] FimDeSemana,
    [Display(Name = "Feriado")] Feriado,
    [Display(Name = "Férias")] Ferias,
    [Display(Name = "Atestado")] Atestado,
    [Display(Name = "Meia Atuação")] MeiaAtuacao
}

public static class TipoDataExtensions
{
    public static string GetDisplayName(this TipoData value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ?? value.ToString();
    }
}