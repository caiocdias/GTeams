using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.GestaoPessoas.Dtos.ColaboradorDtos;

public class ColaboradorLoginDto
{
    [Required] 
    public string User { get; set; } = string.Empty;

    [Required] 
    public string Password { get; set; } = string.Empty;
}