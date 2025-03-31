using GTeams_backend.Dtos.EquipeColaboradorDtos;
using GTeams_backend.Extensions;
using GTeams_backend.Models;
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

    [HttpDelete("RemoverColaboradorEmEquipe")]
    public async Task<IActionResult> RemoverColaboradorEmEquipe(int colaboradorId, int equipeId)
    {
        try
        {
            RetornarEquipeColaboradorDto retornarEquipeColaboradorDto = await equipeColaboradorService.RemoverColaboradorEmEquipeAsync(colaboradorId, equipeId);
            return Ok(retornarEquipeColaboradorDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterEquipeColaboradorPorId")]
    public async Task<IActionResult> ObterEquipeColaboradorPorId([FromBody] int colaboradorId, int equipeId)
    {
        try
        {
            EquipeColaborador? equipeColaborador = await equipeColaboradorService.ObterEquipeColaboradorPorIdAsync(colaboradorId, equipeId);
            if (equipeColaborador == null)
                return NotFound();
            
            return Ok(equipeColaborador.ToReturnDto());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterTodosEquipeColaborador")]
    public async Task<IActionResult> ObterTodosEquipeColaborador()
    {
        try
        {
            List<EquipeColaborador> listEquipeColaborador =
                await equipeColaboradorService.ObterTodosEquipeColaboradorAsync();
            
            List<RetornarEquipeColaboradorDto> retornarEquipeColaboradorDto = listEquipeColaborador.Select(ec => ec.ToReturnDto()).ToList();
            return Ok(retornarEquipeColaboradorDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}