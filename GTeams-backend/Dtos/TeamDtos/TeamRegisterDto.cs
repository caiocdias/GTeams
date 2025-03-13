using GTeams_backend.Models;

namespace GTeams_backend.Dtos.TeamDtos;

public class TeamRegisterDto
{
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public decimal ServiceNoteGoal { get; set; }
    public decimal ServiceUnitGoal { get; set; }
    public int DateDimensionId { get; set; }
}