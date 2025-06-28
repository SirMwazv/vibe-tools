using System.ComponentModel.DataAnnotations;

namespace VibeTools.Models.DTOs;
    public class CreateReviewDto
    {
        [Range(1, 5)]
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string ReviewerName { get; set; } = string.Empty;
    }
