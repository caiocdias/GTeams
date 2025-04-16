using GTeams_backend.GestaoMetas.Dtos.ColaboradorEquipeMetaMensalDtos;
using GTeams_backend.GestaoMetas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.GestaoMetas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColaboradorEquipeMetaMensalController(ColaboradorEquipeMetaMensalService service) : ControllerBase
{
    [HttpPost("InserirColaboradorEquipeMetaMensal")]
    public async Task<IActionResult> Inserir([FromBody] InserirColaboradorEquipeMetaMensalDto dto)
    {
        try
        {
            if (dto is null || !ModelState.IsValid)
                return BadRequest(ModelState);

            var retorno = await service.InserirColaboradorEquipeMetaMensalAsync(dto);
            return Ok(retorno);
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
            var retorno = await service.ObterColaboradorEquipeMetaMensalPorIdAsync(id);
            return Ok(retorno);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterTodosPorColaborador/{colaboradorId}")]
    public async Task<IActionResult> ObterTodosPorColaborador(int colaboradorId)
    {
        try
        {
            var lista = await service.ObterTodosPorColaboradorIdAsync(colaboradorId);
            return Ok(lista);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterTodosPorEquipeMetaMensal/{equipeMetaMensalId}")]
    public async Task<IActionResult> ObterTodosPorEquipeMetaMensal(int equipeMetaMensalId)
    {
        try
        {
            var lista = await service.ObterTodosPorEquipeMetaMensalIdAsync(equipeMetaMensalId);
            return Ok(lista);
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
            var retorno = await service.DeletarColaboradorEquipeMetaMensalAsync(id);
            return Ok(retorno);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
