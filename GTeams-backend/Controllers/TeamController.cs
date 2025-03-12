using GTeams_backend.Dtos.TeamDtos;
using GTeams_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeamController(TeamService teamService) : ControllerBase
{
    [HttpPost("AddTeamAsync")]
    public async Task<ActionResult<TeamReturnDto>> AddTeamAsync(TeamRegisterDto teamRegisterDto)
    {
        try
        {
            TeamReturnDto teamReturnDto = await teamService.AddTeamAsync(teamRegisterDto);
            return Ok(teamReturnDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        
    }
}