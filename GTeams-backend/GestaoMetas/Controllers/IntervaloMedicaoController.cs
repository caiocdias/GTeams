using GTeams_backend.GestaoMetas.Dtos.IntervaloMedicaoDtos;
using GTeams_backend.GestaoMetas.Extensions;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoMetas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.GestaoMetas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntervaloMedicaoController(IntervaloMedicaoService intervaloMedicaoService) : ControllerBase
{
    [HttpPost("Inserir")]
    public async Task<IActionResult> Inserir([FromBody] InserirIntervaloMedicaoDto? inserirIntervaloMedicaoDto)
    {
        try
        {
            if (inserirIntervaloMedicaoDto == null || !ModelState.IsValid)
                return BadRequest();
            
            RetornarIntervaloMedicaoDto retornarIntervaloMedicaoDto = await intervaloMedicaoService.InserirIntervaloMedicao(inserirIntervaloMedicaoDto);
            return Ok(retornarIntervaloMedicaoDto);
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
            IntervaloMedicao? intervaloMedicao  = await intervaloMedicaoService.ObterIntervaloMedicaoPorId(id);
            if (intervaloMedicao == null)
                return NotFound();
            
            return Ok(intervaloMedicao.ToReturnDto());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
        
    }

    [HttpGet("ObterTodos")]
    public async Task<IActionResult> ObterTodos()
    {
        try
        {
            List<IntervaloMedicao> intervaloMedicoes = await intervaloMedicaoService.ObterTodosIntervalosMedicao();
            List<RetornarIntervaloMedicaoDto> intervaloMedicoesDto = intervaloMedicoes.Select(i => i.ToReturnDto()).ToList();
            return Ok(intervaloMedicoesDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("Deletar/{id}")]
    public async Task<IActionResult> Deletar(int id)
    {
        try
        {
            if (await intervaloMedicaoService.DeletarInvervaloMedicaoAsync(id))
                return Ok();
            
            return BadRequest();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}