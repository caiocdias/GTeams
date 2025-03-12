using GTeams_backend.Models;

namespace GTeams_backend.Dtos.DateDimensionDtos;

public class DateDimensionReturnDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int TotalBusinessDays { get; set; }
    public ICollection<Team> Teams { get; set; } = new List<Team>();
}