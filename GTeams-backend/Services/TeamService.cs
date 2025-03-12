using GTeams_backend.Data;
using GTeams_backend.Dtos.TeamDtos;
using GTeams_backend.Models;

namespace GTeams_backend.Services;

public class TeamService(AppDbContext dbContext)
{
    public async Task<TeamReturnDto> AddTeamAsync(TeamRegisterDto teamRegisterDto)
    {
        var dateDimension = await dbContext.DateDimensions.FindAsync(teamRegisterDto.DateDimension);
        if (dateDimension == null)
            throw new KeyNotFoundException("Invalid dateDimension");
        
        Team team = new Team
        {
            Name = teamRegisterDto.Name,
            IsActive = teamRegisterDto.IsActive,
            ServiceNoteGoal = teamRegisterDto.ServiceNoteGoal,
            ServiceUnitGoal = teamRegisterDto.ServiceUnitGoal,
            DateDimension = dateDimension,
            Workers = new List<Worker>(),
            Occurrences = new List<Occurrence>()
        };
        
        dbContext.Teams.Add(team);
        await dbContext.SaveChangesAsync();

        return new TeamReturnDto
        {
            Name = team.Name,
            IsActive = team.IsActive,
            ServiceNoteGoal = team.ServiceNoteGoal,
            ServiceUnitGoal = team.ServiceUnitGoal,
            DateDimension = team.DateDimension,
            Workers = team.Workers,
            Occurrences = team.Occurrences
        };
    }
}