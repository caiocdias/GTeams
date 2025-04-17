using GTeams_backend.GestaoMetas.Dtos.EquipeDtos;
using GTeams_backend.GestaoMetas.Extensions;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoMetas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.GestaoMetas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipeController(EquipeService equipeService) : ControllerBase
{
    [HttpPost("Inserir")]
    public async Task<IActionResult> Inserir([FromBody] InserirEquipeDto? equipeDto)
    {
        try
        {
            if (equipeDto == null || !ModelState.IsValid)
                return BadRequest();

            RetornarEquipeDto retornarEquipeDto = await equipeService.InserirAsync(equipeDto);
            return Ok(retornarEquipeDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterPorId/{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        try
        {
            Equipe? equipe = await equipeService.ObterPorIdAsync(id);
            if (equipe == null)
                return NotFound();

            return Ok(equipe.ToReturnDto());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Desativar/{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        try
        {
            RetornarEquipeDto retornarEquipeDto = await equipeService.DesativarAsync(id);
            return Ok(retornarEquipeDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterTodas")]
    public async Task<IActionResult> ObterTodas()
    {
        try
        {
            List<Equipe> equipes = await equipeService.ObterTodasAsync();
            List<RetornarEquipeDto> equipesDto = equipes.Select(e => e.ToReturnDto()).ToList();
            return Ok(equipesDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}