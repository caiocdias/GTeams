using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<EquipeMetaMensal> EquipesMetasMensais { get; set; }
    public DbSet<IntervaloMedicao> IntervalosMedicao { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Observacao> Observacoes { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<ColaboradorEquipeMetaMensal> ColaboradoresEquipesMetasMensais { get; set; }

    public DbSet<DataPersonalizada> DatasPersonalizadas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Colaborador>()
            .Property(c => c.Funcao)
            .HasConversion<string>();

        modelBuilder.Entity<Matricula>()
            .HasIndex(m => m.Codigo)
            .IsUnique();

        modelBuilder.Entity<ColaboradorEquipeMetaMensal>()
            .HasIndex(c => new { c.ColaboradorId, c.EquipeMetaMensalId })
            .IsUnique();

        modelBuilder.Entity<DataPersonalizada>()
            .Property(d => d.TipoData)
            .HasConversion<string>();

        modelBuilder.Entity<DataPersonalizada>()
            .HasDiscriminator<string>("TipoEntidade")
            .HasValue("Colaborador")
            .HasValue("Medicao");

        modelBuilder.Entity<DataPersonalizada>()
            .HasOne(d => d.Colaborador)
            .WithMany(c => c.DatasPersonalizadasColaborador)
            .HasForeignKey(d => d.ColaboradorId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<DataPersonalizada>()
            .HasOne(d => d.IntervaloMedicao)
            .WithMany(i => i.DatasPersonalizadasMedicao)
            .HasForeignKey(d => d.IntervaloMedicaoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}