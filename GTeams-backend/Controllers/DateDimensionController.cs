using Microsoft.AspNetCore.Mvc;
using GTeams_backend.Dtos.DateDimensionDtos;
using GTeams_backend.Models;
using GTeams_backend.Services;

namespace GTeams_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DateDimensionController(DateDimensionService dateDimensionService) : ControllerBase
{
    [HttpPost("AddDateDimensionAsync")]
    public async Task<ActionResult> AddDateDimensionAsync([FromBody] DateDimensionRegisterDto dateDimensionRegisterDto)
    {
        try
        {
            DateDimensionReturnDto dateDimensionReturnDto = await dateDimensionService.AddDateDimensionAsync(dateDimensionRegisterDto);
            return Ok(dateDimensionReturnDto);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("GetAllDateDimensionAsync")]
    public async Task<ActionResult<List<DateDimensionReturnDto>>> GetAllDateDimensionAsync()
    {
        var dateDimensions = await dateDimensionService.GetAllDateDimensionAsync();
        return Ok(dateDimensions);
    }
    
    
}