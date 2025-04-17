using GTeams_backend.GestaoMetas.Models;
using GTeams_backend.GestaoPessoas.Models;
using Microsoft.EntityFrameworkCore;

namespace GTeams_backend.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Equipe> Equipes { get; set; }
    public DbSet<IntervaloMedicao> IntervalosMedicao { get; set; }
    public DbSet<Matricula> Matriculas { get; set; }
    public DbSet<Observacao> Observacoes { get; set; }
    public DbSet<Email> Emails { get; set; }
    public DbSet<DataPersonalizada> DatasPersonalizadas { get; set; }
    public DbSet<MetaColaboradorMedicao> MetasColaboradoresMedicao { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Colaborador>()
            .Property(c => c.Funcao)
            .HasConversion<string>();

        modelBuilder.Entity<Matricula>()
            .HasIndex(m => m.Codigo)
            .IsUnique();

        modelBuilder.Entity<DataPersonalizada>()
            .Property(d => d.TipoData)
            .HasConversion<string>();

        modelBuilder.Entity<MetaColaboradorMedicao>()
            .HasMany(mcm => mcm.DatasPersonalizadasMedicao)
            .WithOne(dp => dp.MetaColaboradorMedicao)
            .HasForeignKey(dp => dp.MetaColaboradorMedicaoId);

        modelBuilder.Entity<Colaborador>()
            .HasMany(c => c.Emails)
            .WithOne(e => e.Colaborador)
            .HasForeignKey(e => e.ColaboradorId);

        modelBuilder.Entity<Colaborador>()
            .HasMany(c => c.Matriculas)
            .WithOne(m => m.Colaborador)
            .HasForeignKey(m => m.ColaboradorId);

        modelBuilder.Entity<Colaborador>()
            .HasMany(c => c.Observacoes)
            .WithOne(o => o.Colaborador)
            .HasForeignKey(o => o.ColaboradorId);

        modelBuilder.Entity<MetaColaboradorMedicao>()
            .HasOne(mcm => mcm.Colaborador)
            .WithMany()
            .HasForeignKey(mcm => mcm.ColaboradorId);

        modelBuilder.Entity<MetaColaboradorMedicao>()
            .HasOne(mcm => mcm.Equipe)
            .WithMany()
            .HasForeignKey(mcm => mcm.EquipeId);

        modelBuilder.Entity<MetaColaboradorMedicao>()
            .HasOne(mcm => mcm.IntervaloMedicao)
            .WithMany()
            .HasForeignKey(mcm => mcm.IntervaloMedicaoId);
        
        modelBuilder.Entity<MetaColaboradorMedicao>()
            .HasIndex(m => new { m.ColaboradorId, m.EquipeId, m.IntervaloMedicaoId })
            .IsUnique();
    }
}
