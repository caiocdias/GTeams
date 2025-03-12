using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class Worker
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    [StringLength(10)]
    public string RegistrationCode { get; set; } = string.Empty;
    
    [Required]
    [ForeignKey("Team")]
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;
    
    public ICollection<WorkerGoal> WorkerGoals { get; set; } = new List<WorkerGoal>();
    public ICollection<Occurrence> Occurrences { get; set; } = new List<Occurrence>();
}