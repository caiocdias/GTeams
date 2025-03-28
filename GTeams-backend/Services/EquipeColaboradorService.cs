using GTeams_backend.Data;
using GTeams_backend.Dtos.EquipeColaboradorDtos;
using GTeams_backend.Extensions;
using GTeams_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.Services;

public class EquipeColaboradorService(AppDbContext appDbContext, ColaboradorService colaboradorService, EquipeService equipeService) 
{
    public async Task<RetornarEquipeColaboradorDto> InserirColaboradorEmEquipeAsync(InserirEquipeColaboradorDto dto)
    {
        Colaborador? colaborador = await colaboradorService.ObterColaboradorPorIdAsync(dto.ColaboradorId);
        if (colaborador is not { Ativo: true })
            throw new InvalidOperationException("Colaborador não encontrado.");

        Equipe? equipe = await equipeService.ObterEquipePorIdAsync(dto.EquipeId);
        if (equipe is not { Ativo: true })
            throw new InvalidOperationException("Equipe não encontrada.");

        bool jaExiste = await appDbContext.EquipesColaboradores
            .AnyAsync(ec => ec.ColaboradorId == dto.ColaboradorId && ec.EquipeId == dto.EquipeId);
        if (jaExiste)
            throw new InvalidOperationException("O colaborador já está vinculado a essa equipe.");

        EquipeColaborador equipeColaborador = new EquipeColaborador
        {
            ColaboradorId = dto.ColaboradorId,
            EquipeId = dto.EquipeId,
            IsLider = dto.IsLider,
            DataEntrada = dto.DataEntrada ?? DateOnly.FromDateTime(DateTime.Now),
            DataSaida = dto.DataSaida
        };

        appDbContext.EquipesColaboradores.Add(equipeColaborador);
        await appDbContext.SaveChangesAsync();

        return equipeColaborador.ToReturnDto();
    }
}