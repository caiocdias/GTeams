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
    [HttpPost("InserirEquipe")]
    public async Task<IActionResult> InserirEquipe([FromBody] InserirEquipeDto? equipeDto)
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

    [HttpGet("ObterEquipePorId/{id}")]
    public async Task<IActionResult> ObterEquipePorId(int id)
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

    [HttpDelete("DesativarEquipe/{id}")]
    public async Task<IActionResult> DesativarEquipe(int id)
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

    [HttpGet("ObterTodasEquipes")]
    public async Task<IActionResult> ObterTodasEquipes()
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