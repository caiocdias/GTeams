namespace GTeams_backend.GestaoMetas.Dtos.EquipeDtos;

public class RetornarEquipeDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    
}