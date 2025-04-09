namespace GTeams_backend.GestaoPessoas.Dtos.ObservacaoDtos;

public class RetornarObservacaoDto
{
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public int ColaboradorId { get; set; }
    public string ColaboradorNome { get; set; } = string.Empty;
    public DateOnly DataInicial { get; set; }
    public DateOnly DataFinal { get; set; }
}