using GTeams_backend.Exports.Services;
using GTeams_backend.GestaoMetas.Dtos.MetaColaboradorMedicaoDtos;
using GTeams_backend.GestaoMetas.Extensions;
using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoMetas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.GestaoMetas.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MetaColaboradorMedicaoController(
    MetaColaboradorMedicaoService metaColaboradorMedicaoService,
    MetaColaboradorMedicaoExporterService metaColaboradorMedicaoExporterService,
    IConfiguration configuration)
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

            RetornarMetaColaboradorMedicaoDto retornarMetaColaboradorMedicaoDto =
                await metaColaboradorMedicaoService.InserirAsync(inserirMetaColaboradorMedicaoDto);
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
            MetaColaboradorMedicao? metaColaboradorMedicao =
                await metaColaboradorMedicaoService.ObterPorIdAsync(colaboradorId, intervaloMedicaoId, equipeId);
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
            RetornarMetaColaboradorMedicaoDto retornarMetaColaboradorMedicaoDto =
                await metaColaboradorMedicaoService.DeletarAsync(id);
            return Ok(retornarMetaColaboradorMedicaoDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterExcelGeral")]
    public async Task<IActionResult> ObterExcelGeral()
    {
        try
        {
            return File(await metaColaboradorMedicaoExporterService.ExportMetaColaboradorMedicaoAsync(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Metas_Colaboradores_Medicao.xlsx");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterExcelDiario")]
    public async Task<IActionResult> ObterExcelDiario()
    {
        try
        {
            return File(await metaColaboradorMedicaoExporterService.ExportMetaColaboradorMedicaoDailyAsync(),
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                "Metas_Colaboradores_Medicao_Diario.xlsx");
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("SalvarPlanilha")]
    public async Task<IActionResult> SalvarPlanilha()
    {
        try
        {
            byte[] planilhaBytes = await metaColaboradorMedicaoExporterService.ExportMetaColaboradorMedicaoAsync();
            byte[] planilhaDiariaBytes =
                await metaColaboradorMedicaoExporterService.ExportMetaColaboradorMedicaoDailyAsync();

            string? exportPath = configuration.GetSection("Exports")["Path"];
            if (string.IsNullOrEmpty(exportPath))
                return StatusCode(500, "Caminho para exportação não configurado.");

            if (!Directory.Exists(exportPath))
                Directory.CreateDirectory(exportPath);

            string fileName = $"Metas_Colaboradores_Medicao.xlsx";
            string fileNameDiaria = $"Metas_Diarias.xlsx";

            string fullPath = Path.Combine(exportPath, fileName);
            string fullPathDiaria = Path.Combine(exportPath, fileNameDiaria);

            await System.IO.File.WriteAllBytesAsync(fullPath, planilhaBytes);
            await System.IO.File.WriteAllBytesAsync(fullPathDiaria, planilhaDiariaBytes);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}