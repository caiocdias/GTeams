using Microsoft.EntityFrameworkCore;
using GTeams_backend.Models;

namespace GTeams_backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<DateDimension> DateDimensions { get; set; } = null!;
    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Worker> Workers { get; set; } = null!;
    public DbSet<WorkerGoal> WorkerGoals { get; set; } = null!;
    public DbSet<Occurrence> Occurrences { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<DateDimension>()
            .HasIndex(dd => dd.Name)
            .IsUnique();
        
        modelBuilder.Entity<DateDimension>()
            .HasMany(dd => dd.Teams)
            .WithOne(t => t.DateDimension)
            .HasForeignKey(t => t.DateDimensionId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Team>()
            .HasIndex(t => t.Name)
            .IsUnique();
        
        modelBuilder.Entity<Team>()
            .HasMany(t => t.Workers)
            .WithOne(w => w.Team)
            .HasForeignKey(w => w.TeamId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Worker>()
            .HasMany(w => w.WorkerGoals)
            .WithOne(wg => wg.Worker)
            .HasForeignKey(wg => wg.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Worker>()
            .HasMany(w => w.Occurrences)
            .WithOne(o => o.Worker)
            .HasForeignKey(o => o.WorkerId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Worker>()
            .HasIndex(w => new { w.Name, w.TeamId })
            .IsUnique();
        
        modelBuilder.Entity<Team>()
            .HasMany(t => t.Workers)
            .WithOne(w => w.Team)
            .HasForeignKey(w => w.TeamId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Team>()
            .HasMany(t => t.Occurrences)
            .WithOne(o => o.Team)
            .HasForeignKey(o => o.TeamId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<WorkerGoal>()
            .Property(wg => wg.GoalStatus)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }
}