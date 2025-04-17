using GTeams_backend.Data;
using GTeams_backend.GestaoPessoas.Dtos.MatriculaDtos;
using GTeams_backend.GestaoPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.GestaoPessoas.Services;

public class MatriculaService(AppDbContext appDbContext)
{
    public async Task<bool> InserirAsync(InserirMatriculaDto matriculaDto)
    {
        Colaborador? colaborador = await appDbContext.Colaboradores
            .Include(c => c.Matriculas)
            .FirstOrDefaultAsync(c => c.Id == matriculaDto.ColaboradorId);

        if (colaborador == null)
            throw new ArgumentNullException(nameof(colaborador));

        bool matriculaJaExiste = await appDbContext.Matriculas
            .AnyAsync(m => m.Codigo == matriculaDto.Codigo && m.ColaboradorId == matriculaDto.ColaboradorId);

        if (matriculaJaExiste)
            throw new InvalidOperationException("Esta matrícula já está cadastrada para o colaborador informado.");

        Matricula novaMatricula = new Matricula
        {
            Codigo = matriculaDto.Codigo,
            Descricao = matriculaDto.Descricao,
            ColaboradorId = matriculaDto.ColaboradorId
        };

        await appDbContext.Matriculas.AddAsync(novaMatricula);
        await appDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeletarAsync(int matriculaId)
    {
        Matricula? matricula = await appDbContext.Matriculas
            .FirstOrDefaultAsync(m => m.Id == matriculaId);

        if (matricula == null)
            throw new InvalidOperationException("Matrícula não encontrada.");

        appDbContext.Matriculas.Remove(matricula);
        await appDbContext.SaveChangesAsync();

        return true;
    }
    
    public async Task<Matricula?> ObterPorIdAsync(int matriculaId)
    {
        Matricula? matricula = await appDbContext.Matriculas
            .FindAsync(matriculaId);
        return matricula;
    }

}