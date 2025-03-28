using GTeams_backend.Dtos.IntervaloMedicaoDtos;
using GTeams_backend.Extensions;
using GTeams_backend.Models;
using GTeams_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IntervaloMedicaoController(IntervaloMedicaoService intervaloMedicaoService) : ControllerBase
{
    [HttpPost("InserirIntervaloMedicao")]
    public async Task<IActionResult> InserirIntervaloMedicao([FromBody] InserirIntervaloMedicaoDto? inserirIntervaloMedicaoDto)
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

    [HttpGet("ObterIntervaloMedicaoPorId/{id}")]
    public async Task<IActionResult> ObterIntervaloMedicaoPorId(int id)
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

    [HttpGet("ObterTodosIntervaloMedicao")]
    public async Task<IActionResult> ObterTodosIntervaloMedicao()
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

    [HttpDelete("DeletarIntervaloMedicao/{id}")]
    public async Task<IActionResult> DeletarIntervaloMedicao(int id)
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