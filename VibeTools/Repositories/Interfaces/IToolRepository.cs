namespace VibeTools.Repositories.Interfaces;

using VibeTools.Models.Entities;
public interface IToolRepository
{
    Task<IEnumerable<Tool>> GetAllAsync(string? search = null);
    Task<Tool?> GetByIdAsync(int id);
    Task<Tool> CreateAsync(Tool tool);
    Task UpdateAsync(Tool tool);
    Task DeleteAsync(int id);
}