using GTeams_backend.Data;
using GTeams_backend.GestaoMetas.Dtos.ColaboradorEquipeMetaMensalDtos;
using GTeams_backend.GestaoMetas.Extensions;
using GTeams_backend.GestaoMetas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoMetas.Services;

public class ColaboradorEquipeMetaMensalService(AppDbContext appDbContext)
{
    public async Task<RetornarColaboradorEquipeMetaMensalDto> InserirColaboradorEquipeMetaMensalAsync(
        InserirColaboradorEquipeMetaMensalDto dto)
    {
        bool jaExiste = await appDbContext.ColaboradoresEquipesMetasMensais
            .AnyAsync(ce => ce.ColaboradorId == dto.ColaboradorId &&
                            ce.EquipeMetaMensalId == dto.EquipeMetaMensalId);

        if (jaExiste)
            throw new InvalidOperationException("Este vínculo já está cadastrado.");

        var novaEntidade = new ColaboradorEquipeMetaMensal
        {
            ColaboradorId = dto.ColaboradorId,
            EquipeMetaMensalId = dto.EquipeMetaMensalId,
            Ativo = dto.Ativo
        };

        await appDbContext.ColaboradoresEquipesMetasMensais.AddAsync(novaEntidade);
        await appDbContext.SaveChangesAsync();

        await CarregarRelacionamentosAsync(novaEntidade);

        return novaEntidade.ToReturnDto();
    }

    public async Task<RetornarColaboradorEquipeMetaMensalDto> ObterColaboradorEquipeMetaMensalPorIdAsync(int id)
    {
        var entidade = await ObterComRelacionamentosAsync(id)
                       ?? throw new KeyNotFoundException("Vínculo não encontrado.");

        return entidade.ToReturnDto();
    }

    public async Task<List<RetornarColaboradorEquipeMetaMensalDto>> ObterTodosPorColaboradorIdAsync(int colaboradorId)
    {
        var entidades = await appDbContext.ColaboradoresEquipesMetasMensais
            .Where(ce => ce.ColaboradorId == colaboradorId)
            .Include(ce => ce.Colaborador)
            .Include(ce => ce.EquipeMetaMensal)
            .ThenInclude(em => em.Equipe)
            .ToListAsync();

        return entidades.Select(ce => ce.ToReturnDto()).ToList();
    }

    public async Task<List<RetornarColaboradorEquipeMetaMensalDto>> ObterTodosPorEquipeMetaMensalIdAsync(
        int equipeMetaMensalId)
    {
        var entidades = await appDbContext.ColaboradoresEquipesMetasMensais
            .Where(ce => ce.EquipeMetaMensalId == equipeMetaMensalId)
            .Include(ce => ce.Colaborador)
            .Include(ce => ce.EquipeMetaMensal)
            .ThenInclude(em => em.Equipe)
            .ToListAsync();

        return entidades.Select(ce => ce.ToReturnDto()).ToList();
    }

    public async Task<RetornarColaboradorEquipeMetaMensalDto> DeletarColaboradorEquipeMetaMensalAsync(int id)
    {
        var entidade = await ObterComRelacionamentosAsync(id)
                       ?? throw new KeyNotFoundException("Vínculo não encontrado.");

        appDbContext.ColaboradoresEquipesMetasMensais.Remove(entidade);
        await appDbContext.SaveChangesAsync();

        return entidade.ToReturnDto();
    }

    // Auxiliares reutilizáveis
    private async Task<ColaboradorEquipeMetaMensal?> ObterComRelacionamentosAsync(int id)
    {
        return await appDbContext.ColaboradoresEquipesMetasMensais
            .Include(ce => ce.Colaborador)
            .Include(ce => ce.EquipeMetaMensal)
            .ThenInclude(em => em.Equipe)
            .FirstOrDefaultAsync(ce => ce.Id == id);
    }

    private async Task CarregarRelacionamentosAsync(ColaboradorEquipeMetaMensal entidade)
    {
        await appDbContext.Entry(entidade)
            .Reference(ce => ce.Colaborador)
            .LoadAsync();

        await appDbContext.Entry(entidade)
            .Reference(ce => ce.EquipeMetaMensal)
            .Query()
            .Include(em => em.Equipe)
            .LoadAsync();
    }
}