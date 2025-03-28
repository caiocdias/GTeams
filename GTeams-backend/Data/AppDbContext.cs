using Microsoft.EntityFrameworkCore;
using GTeams_backend.Models;

namespace GTeams_backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<EquipeColaborador> EquipesColaboradores { get; set; }
    public DbSet<EquipeMetaMensal> EquipesMetasMensais { get; set; }
    public DbSet<IntervaloMedicao> IntervalosMedicao { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Observacao> Observacoes { get; set; }
    public DbSet<DataPersonalizadaColaborador> DatasPersonalizadasColaborador { get; set; }
    public DbSet<DataPersonalizadaMedicao> DatasPersonalizadasMedicao { get; set; }
    public DbSet<Email> Emails { get; set; }

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

        modelBuilder.Entity<EquipeColaborador>()
            .HasKey(ec => ec.Id);

        modelBuilder.Entity<EquipeColaborador>()
            .HasOne(ec => ec.Colaborador)
            .WithMany(c => c.EquipesColaboradores)
            .HasForeignKey(ec => ec.ColaboradorId);

        modelBuilder.Entity<EquipeColaborador>()
            .HasOne(ec => ec.Equipe)
            .WithMany(e => e.EquipesColaboradores)
            .HasForeignKey(ec => ec.EquipeId);
        
        modelBuilder.Entity<EquipeColaborador>()
            .HasIndex(ec => new { ec.ColaboradorId, ec.EquipeId })
            .IsUnique();
        
        modelBuilder.Entity<Matricula>()
            .HasIndex(m => m.Codigo)
            .IsUnique();
    }

}
