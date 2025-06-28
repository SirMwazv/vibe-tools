using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VibeTools.Models.Entities;

public class Tool
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public bool IsVisible { get; set; } = true;
    public bool IsCommunityFavorite { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
    
    [NotMapped]
    public double AverageRating => Reviews.Any() ? Reviews.Average(r => r.Rating) : 0;
    
    [NotMapped]
    public int ReviewCount => Reviews.Count;
}