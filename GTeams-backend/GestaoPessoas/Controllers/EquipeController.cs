using GTeams_backend.GestaoPessoas.Dtos.EquipeDtos;
using GTeams_backend.GestaoPessoas.Extensions;
using GTeams_backend.GestaoPessoas.Models;
using GTeams_backend.GestaoPessoas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.GestaoPessoas.Controllers;

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

            RetornarEquipeDto retornarEquipeDto = await equipeService.InserirEquipe(equipeDto);
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
            Equipe? equipe = await equipeService.ObterEquipePorIdAsync(id);
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
            RetornarEquipeDto retornarEquipeDto = await equipeService.DesativarEquipeAsync(id);
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
            List<Equipe> equipes = await equipeService.ObterTodasEquipesAsync();
            List<RetornarEquipeDto> equipesDto = equipes.Select(e => e.ToReturnDto()).ToList();
            return Ok(equipesDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}