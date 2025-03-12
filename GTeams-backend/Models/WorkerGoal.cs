using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GTeams_backend.Models.Enums;

namespace GTeams_backend.Models;

public class WorkerGoal
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public DateOnly Date { get; set; }

    [Required]
    [ForeignKey("Worker")]
    public int WorkerId { get; set; }
    public Worker Worker { get; set; } = null!;

    [Required]
    public decimal DailyGoalUS { get; set; }

    [Required]
    public decimal DailyGoalNS { get; set; }

    [Required]
    public GoalStatus GoalStatus { get; set; }
}