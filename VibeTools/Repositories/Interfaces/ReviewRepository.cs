using Microsoft.EntityFrameworkCore;
using VibeTools.Data;
using VibeTools.Models.Entities;
using VibeTools.Repositories.Interfaces;

namespace VibeTools.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly VibeToolsContext _context;

    public ReviewRepository(VibeToolsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Review>> GetAllAsync()
    {
        return await _context.Reviews
            .Include(r => r.Tool)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Review>> GetByToolIdAsync(int toolId)
    {
        return await _context.Reviews
            .Where(r => r.ToolId == toolId)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync();
    }

    public async Task<Review?> GetByIdAsync(int id)
    {
        return await _context.Reviews
            .Include(r => r.Tool)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Review> CreateAsync(Review review)
    {
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return review;
    }

    public async Task UpdateAsync(Review review)
    {
        _context.Reviews.Update(review);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review != null)
        {
            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<double> GetAverageRatingByToolIdAsync(int toolId)
    {
        var reviews = await _context.Reviews
            .Where(r => r.ToolId == toolId)
            .ToListAsync();
        
        return reviews.Any() ? reviews.Average(r => r.Rating) : 0;
    }

    public async Task<int> GetReviewCountByToolIdAsync(int toolId)
    {
        return await _context.Reviews
            .CountAsync(r => r.ToolId == toolId);
    }
}