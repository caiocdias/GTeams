using GTeams_backend.GestaoMetas.Dtos.EquipeMetaMensalDtos;
using GTeams_backend.GestaoMetas.Extensions;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoMetas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.GestaoMetas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EquipeMetaMensalController(EquipeMetaMensalService equipeMetaMensalService) : ControllerBase
{
    [HttpPost("InserirEquipeMetaMensal")]
    public async Task<IActionResult> InserirEquipeMetaMensal([FromBody] InserirEquipeMetaMensalDto inserirEquipeMetaMensalDto)
    {
        try
        {
            RetornarEquipeMetaMensalDto retornarEquipeMetaMensalDto = await equipeMetaMensalService.InserirEquipeMetaMensalAsync(inserirEquipeMetaMensalDto);
            return Ok(retornarEquipeMetaMensalDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterEquipeMetaMensalPorId")]
    public async Task<IActionResult> ObterEquipeMetaMensalPorId([FromBody] int equipeId, int intervaloMedicaoId)
    {
        try
        {
            EquipeMetaMensal? equipeMetaMensal = await equipeMetaMensalService.ObterEquipeMetaMensalPorIdAsync(equipeId, intervaloMedicaoId);
            if (equipeMetaMensal == null)
                return NotFound();
            
            return Ok(equipeMetaMensal.ToReturnDto());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterTodosEquipeMetaMensal")]
    public async Task<IActionResult> ObterTodosEquipeMetaMensal()
    {
        try
        {
            List<EquipeMetaMensal> equipesMetasMensais =
                await equipeMetaMensalService.ObterTodosEquipeMetaMensalAsync();
            
            return Ok(equipesMetasMensais.Select(emm => emm.ToReturnDto()).ToList());
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("DeletarEquipeMetaMensal")]
    public async Task<IActionResult> DeletarEquipeMetaMensal([FromBody] int equipeId, int intervaloMedicaoId)
    {
        try
        {
            RetornarEquipeMetaMensalDto retornarEquipeMetaMensalDto = await equipeMetaMensalService.DeletarEquipeMetaMensalAsync(equipeId, intervaloMedicaoId);
            return Ok(retornarEquipeMetaMensalDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}