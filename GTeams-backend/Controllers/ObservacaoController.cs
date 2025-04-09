using GTeams_backend.GestaoMetas.Services;
using GTeams_backend.GestaoPessoas.Dtos.ObservacaoDtos;
using GTeams_backend.GestaoPessoas.Services;
using GTeams_backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ObservacaoController(ObservacaoService observacaoService) : ControllerBase
{
    [HttpPost("InserirObservacao")]
    public async Task<IActionResult> InserirObservacao([FromBody] InserirObservacaoDto inserirObservacaoDto)
    {
        try
        {
            RetornarObservacaoDto retornarObservacaoDto = await observacaoService.InserirObservacaoAsync(inserirObservacaoDto);
            return Ok(retornarObservacaoDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterObservacaoPorId/{id}")]
    public async Task<IActionResult> ObterObservacaoPorId(int id)
    {
        try
        {
            Observacao? retornarObservacaoDto = await observacaoService.ObterObservacaoPorIdAsync(id);
            if (retornarObservacaoDto == null)
                return NotFound();
            
            return Ok(retornarObservacaoDto.ToReturnDto());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterTodasObservacoes")]
    public async Task<IActionResult> ObterTodasObservacoes()
    {
        try
        {
            List<Observacao> observacoes = await observacaoService.ObterTodasObservacoesAsync();
            return Ok(observacoes.Select(o => o.ToReturnDto()).ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("DeletarObservacao/{id}")]
    public async Task<IActionResult> DeletarObservacao(int id)
    {
        try
        {
            RetornarObservacaoDto retornarObservacaoDto = await observacaoService.DeletarObservacaoAsync(id);
            return Ok(retornarObservacaoDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}