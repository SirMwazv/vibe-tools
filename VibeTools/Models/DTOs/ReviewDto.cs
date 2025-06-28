namespace VibeTools.Models.DTOs;

public class ReviewDto
{
    public int Id { get; set; }
    public int ToolId { get; set; }
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public string ReviewerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}