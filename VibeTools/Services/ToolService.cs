using VibeTools.Models.DTOs;
using VibeTools.Models.Entities;
using VibeTools.Repositories.Interfaces;
using VibeTools.Services.Interfaces;

namespace VibeTools.Services;

public class ToolService : IToolService
{
    private readonly IToolRepository _toolRepository;
    private readonly IReviewRepository _reviewRepository;

    public ToolService(IToolRepository toolRepository, IReviewRepository reviewRepository)
    {
        _toolRepository = toolRepository;
        _reviewRepository = reviewRepository;
    }

    public async Task<IEnumerable<ToolDto>> GetAllToolsAsync(string? search = null)
    {
        var tools = await _toolRepository.GetAllAsync(search);
        
        // Update tool statuses
        foreach (var tool in tools)
        {
            UpdateToolStatus(tool);
        }
        
        // Filter visible tools and order by ranking score
        return tools.Where(t => t.IsVisible)
                   .Select(MapToDto)
                   .OrderByDescending(t => CalculateRankingScore(t.AverageRating, t.ReviewCount))
                   .ThenByDescending(t => t.AverageRating)
                   .ThenByDescending(t => t.ReviewCount);
    }

    public async Task<ToolDto?> GetToolByIdAsync(int id)
    {
        var tool = await _toolRepository.GetByIdAsync(id);
        if (tool == null) return null;
        
        UpdateToolStatus(tool);
        await _toolRepository.UpdateAsync(tool);
        
        return MapToDto(tool);
    }

    public async Task<ToolDto> CreateToolAsync(CreateToolDto dto)
    {
        var tool = new Tool
        {
            Name = dto.Name,
            Description = dto.Description,
            Category = dto.Category,
            Url = dto.Url,
            IsVisible = true,
            IsCommunityFavorite = false,
            CreatedAt = DateTime.UtcNow
        };

        var createdTool = await _toolRepository.CreateAsync(tool);
        return MapToDto(createdTool);
    }

    public async Task<ToolDto?> UpdateToolAsync(int id, CreateToolDto dto)
    {
        var existingTool = await _toolRepository.GetByIdAsync(id);
        if (existingTool == null)
            return null;

        existingTool.Name = dto.Name;
        existingTool.Description = dto.Description;
        existingTool.Category = dto.Category;
        existingTool.Url = dto.Url;

        await _toolRepository.UpdateAsync(existingTool);
        return MapToDto(existingTool);
    }

    public async Task<bool> DeleteToolAsync(int id)
    {
        var tool = await _toolRepository.GetByIdAsync(id);
        if (tool == null)
            return false;

        await _toolRepository.DeleteAsync(id);
        return true;
    }

    public async Task<ReviewDto?> CreateReviewAsync(int toolId, CreateReviewDto dto)
    {
        var tool = await _toolRepository.GetByIdAsync(toolId);
        if (tool == null)
            return null;

        var review = new Review
        {
            ToolId = toolId,
            Rating = dto.Rating,
            Comment = dto.Comment,
            ReviewerName = dto.ReviewerName,
            CreatedAt = DateTime.UtcNow
        };

        var createdReview = await _reviewRepository.CreateAsync(review);
        return MapReviewToDto(createdReview);
    }

    public async Task<IEnumerable<ReviewDto>> GetToolReviewsAsync(int toolId)
    {
        var reviews = await _reviewRepository.GetByToolIdAsync(toolId);
        return reviews.Select(MapReviewToDto);
    }

    private static ToolDto MapToDto(Tool tool)
    {
        var rankingScore = CalculateRankingScore(tool.AverageRating, tool.ReviewCount);
        
        return new ToolDto
        {
            Id = tool.Id,
            Name = tool.Name,
            Description = tool.Description,
            Category = tool.Category,
            Url = tool.Url,
            IsCommunityFavorite = tool.IsCommunityFavorite,
            AverageRating = tool.AverageRating,
            ReviewCount = tool.ReviewCount,
            RankingScore = rankingScore,
            CreatedAt = tool.CreatedAt,
            Reviews = tool.Reviews?.Select(MapReviewToDto).ToList() ?? new List<ReviewDto>()
        };
    }

    private static ReviewDto MapReviewToDto(Review review)
    {
        return new ReviewDto
        {
            Id = review.Id,
            ToolId = review.ToolId,
            Rating = review.Rating,
            Comment = review.Comment,
            ReviewerName = review.ReviewerName,
            CreatedAt = review.CreatedAt
        };
    }

    private static double CalculateRankingScore(double averageRating, int reviewCount)
    {
        // Bayesian average with confidence factor
        // The more reviews, the more the score approaches the actual average
        // Tools with fewer reviews get penalized slightly
        
        const double globalAverageRating = 3.0; // Assume global average of 3.0
        const double minimumReviewsForFullWeight = 10.0; // Reviews needed for full weight
        
        // Calculate confidence factor (0 to 1)
        var confidence = Math.Min(reviewCount / minimumReviewsForFullWeight, 1.0);
        
        // Bayesian weighted rating
        var bayesianRating = (confidence * averageRating) + ((1 - confidence) * globalAverageRating);
        
        // Add bonus for having more reviews (logarithmic scale to prevent runaway)
        var reviewBonus = Math.Log10(Math.Max(reviewCount, 1)) * 0.1;
        
        // Final score: Bayesian rating + review bonus
        return Math.Round(bayesianRating + reviewBonus, 2);
    }

    private static void UpdateToolStatus(Tool tool)
    {
        var latestReviews = tool.Reviews.OrderByDescending(r => r.CreatedAt).Take(5).ToList();
        
        if (latestReviews.Count == 5)
        {
            if (latestReviews.All(r => r.Rating == 5))
            {
                tool.IsCommunityFavorite = true;
            }
            else if (latestReviews.All(r => r.Rating == 1))
            {
                tool.IsVisible = false;
            }
            else
            {
                tool.IsCommunityFavorite = false;
            }
        }
    }
}