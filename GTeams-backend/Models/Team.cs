using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTeams_backend.Models;

public class Team
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
    
    [Required]
    public decimal ServiceNoteGoal { get; set; }
    
    [Required]
    public decimal ServiceUnitGoal { get; set; }
    
    [Required]
    [ForeignKey("DateDimension")]
    public int DateDimensionId { get; set; }
    public DateDimension DateDimension { get; set; } = null!;
    
    public ICollection<Worker> Workers { get; set; } = new List<Worker>();
    public ICollection<Occurrence> Occurrences { get; set; } = new List<Occurrence>();
}