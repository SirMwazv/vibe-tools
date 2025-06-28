using VibeTools.Models.Entities;

namespace VibeTools.Repositories.Interfaces;

public interface IReviewRepository
{
    Task<IEnumerable<Review>> GetAllAsync();
    Task<IEnumerable<Review>> GetByToolIdAsync(int toolId);
    Task<Review?> GetByIdAsync(int id);
    Task<Review> CreateAsync(Review review);
    Task UpdateAsync(Review review);
    Task DeleteAsync(int id);
    Task<double> GetAverageRatingByToolIdAsync(int toolId);
    Task<int> GetReviewCountByToolIdAsync(int toolId);
}