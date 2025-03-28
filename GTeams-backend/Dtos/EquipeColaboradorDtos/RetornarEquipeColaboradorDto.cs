using System.ComponentModel.DataAnnotations;

namespace GTeams_backend.Dtos.EquipeColaboradorDtos;

public class RetornarEquipeColaboradorDto
{
    public int Id { get; set; }

    public string NomeColaborador { get; set; } = string.Empty;

    public string NomeEquipe { get; set; } = string.Empty;

    public bool? IsLider { get; set; }

    public DateOnly? DataEntrada { get; set; }

    public DateOnly? DataSaida { get; set; }
}