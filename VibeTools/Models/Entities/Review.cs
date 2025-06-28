using System.ComponentModel.DataAnnotations;

namespace VibeTools.Models.Entities;

public class Review
{
    public int Id { get; set; }
    [Required]
    public int ToolId { get; set; }
    [Range(1, 5)]
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string ReviewerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual Tool Tool { get; set; } = null!;
}