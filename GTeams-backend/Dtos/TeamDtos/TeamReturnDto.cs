using GTeams_backend.Models;

namespace GTeams_backend.Dtos.TeamDtos;

public class TeamReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public decimal ServiceNoteGoal { get; set; }
    public decimal ServiceUnitGoal { get; set; }
    public DateDimension DateDimension { get; set; } = null!;
    public ICollection<Worker> Workers { get; set; } = new List<Worker>();
    public ICollection<Occurrence> Occurrences { get; set; } = new List<Occurrence>();
}