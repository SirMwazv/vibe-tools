using VibeTools.Models.DTOs;

namespace VibeTools.Services.Interfaces;

public interface IToolService
{
    Task<IEnumerable<ToolDto>> GetAllToolsAsync(string? search = null);
    Task<ToolDto?> GetToolByIdAsync(int id);
    Task<ToolDto> CreateToolAsync(CreateToolDto dto);
    Task<ReviewDto> CreateReviewAsync(int toolId, CreateReviewDto dto);
    Task<IEnumerable<ReviewDto>>  GetToolReviewsAsync (int toolId);
    Task<ToolDto> UpdateToolAsync(int id, CreateToolDto dto);

}