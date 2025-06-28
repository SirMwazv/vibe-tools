using Microsoft.EntityFrameworkCore;
using VibeTools.Data;
using VibeTools.Models.Entities;
using VibeTools.Repositories.Interfaces;

namespace VibeTools.Repositories;

public class ToolRepository : IToolRepository
{
    private readonly VibeToolsContext _context;

    public ToolRepository(VibeToolsContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Tool>> GetAllAsync(string? search = null)
    {
        var query = _context.Tools.Include(t => t.Reviews).Where(t => t.IsVisible);
        
        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(t => t.Name.Contains(search) || 
                                   t.Description.Contains(search) || 
                                   t.Category.Contains(search));
        }
        
        return await query.ToListAsync();
    }

    public async Task<Tool?> GetByIdAsync(int id)
    {
        return await _context.Tools
            .Include(t => t.Reviews)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<Tool> CreateAsync(Tool tool)
    {
        _context.Tools.Add(tool);
        await _context.SaveChangesAsync();
        return tool;
    }

    public async Task UpdateAsync(Tool tool)
    {
        _context.Tools.Update(tool);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tool = await _context.Tools.FindAsync(id);
        if (tool != null)
        {
            _context.Tools.Remove(tool);
            await _context.SaveChangesAsync();
        }
    }
}