using GTeams_backend.GestaoPessoas.Dtos.EmailDtos;
using GTeams_backend.GestaoPessoas.Dtos.MatriculaDtos;

namespace GTeams_backend.GestaoPessoas.Dtos.ColaboradorDtos;

public class RetornarColaboradorDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string User { get; set; } = string.Empty;
    public string Cpf { get; set; } = "000.000.000-00";
    public bool Ativo { get; set; } = true;
    public string Funcao { get; set; } = string.Empty;
    public ICollection<RetornarMatriculaDto> Matriculas { get; set; } = new List<RetornarMatriculaDto>();
    public ICollection<RetornarEmailDto> Emails { get; set; } = new List<RetornarEmailDto>();
}