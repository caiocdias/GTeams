using GTeams_backend.Data;
using GTeams_backend.Dtos.TeamDtos;
using GTeams_backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
    
    public async Task<List<TeamReturnDto>> GettAllTeamsAsync()
    {
        List<Team> teams = await dbContext.Teams
            .Include(t => t.DateDimension)
            .Include(t => t.Workers)
            .Include(t => t.Occurrences)
            .ToListAsync();

        return teams.Select(t => new TeamReturnDto
        {
            Id = t.Id,
            Name = t.Name,
            IsActive = t.IsActive,
            ServiceNoteGoal = t.ServiceNoteGoal,
            ServiceUnitGoal = t.ServiceUnitGoal,
            DateDimension = t.DateDimension,
            Workers = t.Workers,
            Occurrences = t.Occurrences
        }).ToList();
    }

    public async Task<TeamReturnDto?> GetTeamByIdAsync(int teamId)
    {
        Team? team = await dbContext.Teams
            .Include(t => t.DateDimension)
            .Include(t => t.Workers)
            .Include(t => t.Occurrences)
            .FirstOrDefaultAsync(t => t.Id == teamId);

        if (team == null)
            return null;

        return new TeamReturnDto
        {
            Id = team.Id,
            Name = team.Name,
            IsActive = team.IsActive,
            ServiceNoteGoal = team.ServiceNoteGoal,
            ServiceUnitGoal = team.ServiceUnitGoal,
            DateDimension = team.DateDimension,
            Workers = team.Workers,
            Occurrences = team.Occurrences
        };
    }
    public async Task<bool> SoftDeleteTeamAsync(int teamId)
    {
        Team? team = await dbContext.Teams.FirstOrDefaultAsync(t => t.Id == teamId);
        if (team == null)
            return false;

        team.IsActive = false;
        await dbContext.SaveChangesAsync();
    
        return true;
    }
}