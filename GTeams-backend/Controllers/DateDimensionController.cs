using Microsoft.AspNetCore.Mvc;
using GTeams_backend.Dtos.DateDimensionDtos;
using GTeams_backend.Models;
using GTeams_backend.Services;

namespace GTeams_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DateDimensionController(DateDimensionService dateDimensionService) : ControllerBase
{
    [HttpPost("AddDateDimension")]
    public async Task<ActionResult> AddDateDimension([FromBody] DateDimensionRegisterDto dateDimensionRegisterDto)
    {
        try
        {
            DateDimensionReturnDto dateDimensionReturnDto = await dateDimensionService.CreateDateDimensionAsync(dateDimensionRegisterDto);
            return Ok(dateDimensionReturnDto);
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}