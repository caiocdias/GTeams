using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace GTeams_wpfapp.GestaoPessoas.Models;

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

public static class TipoDataExtensions
{
    public static string GetDisplayName(this Funcao value)
    {
        return value.GetType()
            .GetMember(value.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()?
            .GetName() ?? value.ToString();
    }
}