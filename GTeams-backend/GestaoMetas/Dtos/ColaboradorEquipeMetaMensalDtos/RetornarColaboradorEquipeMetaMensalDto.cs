namespace GTeams_backend.GestaoMetas.Dtos.ColaboradorEquipeMetaMensalDtos;

public class RetornarColaboradorEquipeMetaMensalDto
{
    public int Id { get; set; }

    public int ColaboradorId { get; set; }
    public string NomeColaborador { get; set; } = string.Empty;

    public int EquipeMetaMensalId { get; set; }
    public string NomeEquipeMetaMensal { get; set; } = string.Empty;

    public bool Ativo { get; set; } = true;
}