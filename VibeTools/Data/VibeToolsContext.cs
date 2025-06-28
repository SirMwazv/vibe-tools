using Microsoft.EntityFrameworkCore;
using VibeTools.Models.Entities;

namespace VibeTools.Data;

public class VibeToolsContext : DbContext
{
    public VibeToolsContext(DbContextOptions<VibeToolsContext> options) : base(options) { }
    
    public DbSet<Tool> Tools { get; set; }
    public DbSet<Review> Reviews { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tool>()
            .HasMany(t => t.Reviews)
            .WithOne(r => r.Tool)
            .HasForeignKey(r => r.ToolId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}