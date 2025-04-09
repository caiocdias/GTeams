using GTeams_backend.GestaoPessoas.Dtos.ColaboradorDtos;
using GTeams_backend.GestaoPessoas.Extensions;
using GTeams_backend.GestaoPessoas.Models;
using GTeams_backend.GestaoPessoas.Services;
using Microsoft.AspNetCore.Mvc;

namespace GTeams_backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ColaboradorController(ColaboradorService colaboradorService, JwtService jwtService) : ControllerBase
{
    [HttpPost("InserirColaborador")]
    public async Task<IActionResult> InserirColaborador([FromBody] InserirColaboradorDto? inserirColaboradorDto)
    {
        try
        {
            if (inserirColaboradorDto is null || !ModelState.IsValid)
                return BadRequest(ModelState);
        
            RetornarColaboradorDto retornarColaboradorDto = await colaboradorService.InserirColaboradorAsync(inserirColaboradorDto);
            return Ok(retornarColaboradorDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("ObterColaboradorPorId/{idColaborador}")]
    public async Task<IActionResult> ObterColaboradorPorId(int idColaborador)
    {
        Colaborador? colaborador = await colaboradorService.ObterColaboradorPorIdAsync(idColaborador);
        if (colaborador is null)
            return NotFound();
        
        return Ok(colaborador.ToReturnDto());
    }

    [HttpGet("ObterTodosColaboradores")]
    public async Task<IActionResult> ObterTodosColaboradores()
    {
        try
        {
            List<Colaborador> colaboradores = await colaboradorService.ObterTodosColaboradoresAsync();

            List<RetornarColaboradorDto> colaboradoresDto = colaboradores
                .Select(c => c.ToReturnDto())
                .ToList();

            return Ok(colaboradoresDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpDelete("DesativarColaborador/{idColaborador}")]
    public async Task<IActionResult> DesativarColaborador(int idColaborador)
    {
        try
        {
            RetornarColaboradorDto colaboradorDesativadoDto =
                await colaboradorService.DesativarColaboradorAsync(idColaborador);
            return Ok(colaboradorDesativadoDto);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] ColaboradorLoginDto loginDto)
    {
        try
        {
            Colaborador? user = await colaboradorService.ObterColaboradorPorMatriculaAsync(loginDto.Matricula);
            if (user == null || !user.ValidatePassword(loginDto.Password))
                return Unauthorized(new { message = "E-mail ou senha inválidos" });

            if (!user.Ativo)
                return StatusCode(403, new { message = "Usuário desativado" });

            string? token = jwtService.GenerateToken(user);

            if (token == null)
                return BadRequest();

            return Ok(new { token });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

    }
}