namespace GTeams_backend.GestaoPessoas.Dtos.ObservacaoDtos;

public class InserirObservacaoDto
{
    public string Descricao { get; set; } = string.Empty;
    public int ColaboradorId { get; set; }
    public DateOnly DataInicial { get; set; }
    public DateOnly DataFinal { get; set; }
}