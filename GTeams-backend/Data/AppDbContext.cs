using Microsoft.EntityFrameworkCore;
using GTeams_backend.Models;

namespace GTeams_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<EquipeMetaMensal> EquipesMetasMensais { get; set; }
    public DbSet<IntervaloMedicao> IntervalosMedicao { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Observacao> Observacoes { get; set; }
    public DbSet<DataPersonalizadaColaborador> DatasPersonalizadasColaborador { get; set; }
    public DbSet<DataPersonalizadaMedicao> DatasPersonalizadasMedicao { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Colaborador>()
            .Property(c => c.Funcao)
            .HasConversion<string>();

        modelBuilder.Entity<DataPersonalizadaColaborador>()
            .Property(d => d.TipoData)
            .HasConversion<string>();

        modelBuilder.Entity<DataPersonalizadaMedicao>()
            .Property(d => d.TipoData)
            .HasConversion<string>();
    }
}