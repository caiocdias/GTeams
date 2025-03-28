using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.Dtos.ColaboradorDtos;

public class ColaboradorLoginDto
{
    [Required] public string Matricula { get; set; } = string.Empty;

    [Required] public string Password { get; set; } = string.Empty;
}