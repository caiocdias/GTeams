namespace GTeams_backend.Dtos.DateDimensionDtos;

public class DateDimensionRegisterDto
{
    public string Name { get; set; } = string.Empty;
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public int TotalBusinessDays { get; set; }
}