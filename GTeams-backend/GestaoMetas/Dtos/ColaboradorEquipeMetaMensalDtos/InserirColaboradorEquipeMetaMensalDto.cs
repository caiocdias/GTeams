namespace GTeams_backend.GestaoMetas.Dtos.ColaboradorEquipeMetaMensalDtos;

public class InserirColaboradorEquipeMetaMensalDto
{
    public int ColaboradorId { get; set; }

    public int EquipeMetaMensalId { get; set; }

    public bool Ativo { get; set; } = true;

}