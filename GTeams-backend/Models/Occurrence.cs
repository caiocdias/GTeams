using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class Occurrence
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(255)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    
    [Required]
    [ForeignKey("Team")]
    public int TeamId { get; set; }
    public Team Team { get; set; } = null!;
    
    [Required]
    [ForeignKey("Worker")]
    public int WorkerId { get; set; }
    public Worker Worker { get; set; } = null!;
}