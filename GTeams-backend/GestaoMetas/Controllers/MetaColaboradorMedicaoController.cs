using GTeams_backend.GestaoMetas.Dtos.MetaColaboradorMedicaoDtos;
using GTeams_backend.GestaoMetas.Extensions;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoMetas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.GestaoMetas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MetaColaboradorMedicaoController(MetaColaboradorMedicaoService metaColaboradorMedicaoService)
    : ControllerBase
{
    [HttpPost("Inserir")]
    public async Task<IActionResult> Inserir(
        [FromBody] InserirMetaColaboradorMedicaoDto? inserirMetaColaboradorMedicaoDto)
    {
        try
        {
            if (inserirMetaColaboradorMedicaoDto is null || !ModelState.IsValid)
            {
                return BadRequest();
            }
            
            RetornarMetaColaboradorMedicaoDto retornarMetaColaboradorMedicaoDto = await metaColaboradorMedicaoService.InserirAsync(inserirMetaColaboradorMedicaoDto);
            return Ok(retornarMetaColaboradorMedicaoDto);
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
            MetaColaboradorMedicao? metaColaboradorMedicao = await metaColaboradorMedicaoService.ObterPorIdAsync(id);
            if (metaColaboradorMedicao == null)
                return NotFound();

            return Ok(metaColaboradorMedicao.ToReturnDto());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterPorRelacionamento")]
    public async Task<IActionResult> ObterPorRelacionamento([FromQuery] int colaboradorId,
        [FromQuery] int intervaloMedicaoId, [FromQuery] int equipeId)
    {
        try
        {
            MetaColaboradorMedicao? metaColaboradorMedicao = await metaColaboradorMedicaoService.ObterPorIdAsync(colaboradorId, intervaloMedicaoId, equipeId);
            if (metaColaboradorMedicao == null)
                return NotFound();
            
            return Ok(metaColaboradorMedicao.ToReturnDto());
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
            RetornarMetaColaboradorMedicaoDto retornarMetaColaboradorMedicaoDto = await metaColaboradorMedicaoService.DeletarAsync(id);
            return Ok(retornarMetaColaboradorMedicaoDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}