using GTeams_backend.Dtos.EquipeColaboradorDtos;
using GTeams_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipeColaboradorController(EquipeColaboradorService equipeColaboradorService) : ControllerBase
{
    [HttpPost("InserirColaboradorEmEquipe")]
    public async Task<IActionResult> InserirColaboradorEmEquipe(InserirEquipeColaboradorDto inserirEquipeColaboradorDto)
    {
        try
        {
            RetornarEquipeColaboradorDto retornarEquipeColaboradorDto = await equipeColaboradorService.InserirColaboradorEmEquipeAsync(inserirEquipeColaboradorDto);
            return Ok(retornarEquipeColaboradorDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}