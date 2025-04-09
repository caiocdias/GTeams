using System.ComponentModel.DataAnnotations;
using GTeams_backend.GestaoMetas.Dtos.ColaboradorEquipeMetaMensalDtos;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoPessoas.Dtos.EmailDtos;
using GTeams_backend.GestaoPessoas.Dtos.MatriculaDtos;
using GTeams_backend.GestaoPessoas.Dtos.ObservacaoDtos;

namespace GTeams_backend.GestaoPessoas.Dtos.ColaboradorDtos;

public class RetornarColaboradorDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome é obrigatório.")]
    [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
    [RegularExpression(@"^[a-zA-ZÀ-ÿ\s'-]+$", ErrorMessage = "O nome contém caracteres inválidos.")]
    public string Nome { get; set; } = string.Empty;

    [StringLength(14)]
    [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "CPF inválido. Use o formato 000.000.000-00.")]
    public string Cpf { get; set; } = "000.000.000-00";

    public bool Ativo { get; set; } = true;

    [Required]
    public string Funcao { get; set; } = string.Empty;

    public ICollection<RetornarObservacaoDto> Observacoes { get; set; } = new List<RetornarObservacaoDto>();

    public ICollection<RetornarMatriculaDto> Matriculas { get; set; } = new List<RetornarMatriculaDto>();

    public ICollection<RetornarEmailDto> Emails { get; set; } = new List<RetornarEmailDto>();

    public ICollection<RetornarColaboradorEquipeMetaMensalDto> ColaboradoresEquipeMetaMensal { get; set; } = new List<RetornarColaboradorEquipeMetaMensalDto>();

    public ICollection<DataPersonalizada> DatasPersonalizadas { get; set; } = new List<DataPersonalizada>();
}