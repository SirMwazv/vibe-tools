using Microsoft.AspNetCore.Mvc;
using VibeTools.Models.DTOs;
using VibeTools.Services.Interfaces;

namespace VibeTools.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ToolsController : ControllerBase
{
    private readonly IToolService _toolService;
    
    public ToolsController(IToolService toolService)
    {
        _toolService = toolService;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ToolDto>>> GetTools([FromQuery] string? search = null)
    {
        var tools = await _toolService.GetAllToolsAsync(search);
        return Ok(tools);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<ToolDto>> GetTool(int id)
    {
        var tool = await _toolService.GetToolByIdAsync(id);
        
        if (tool == null)
            return NotFound();
        
        return Ok(tool);
    }
    
    [HttpPost]
    public async Task<ActionResult<ToolDto>> CreateTool(CreateToolDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        var tool = await _toolService.CreateToolAsync(dto);
        return CreatedAtAction(nameof(GetTool), new { id = tool.Id }, tool);
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ToolDto>> UpdateTool(int id, CreateToolDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        var tool = await _toolService.UpdateToolAsync(id, dto);
        
        if (tool == null)
            return NotFound();
            
        return Ok(tool);
    }
    
    [HttpPost("{id}/reviews")]
    public async Task<ActionResult<ReviewDto>> CreateReview(int id, CreateReviewDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
            
        var review = await _toolService.CreateReviewAsync(id, dto);
        
        if (review == null)
            return NotFound("Tool not found");
        
        return Ok(review);
    }
    
    [HttpGet("{id}/reviews")]
    public async Task<ActionResult<IEnumerable<ReviewDto>>> GetToolReviews(int id)
    {
        var tool = await _toolService.GetToolByIdAsync(id);
        if (tool == null)
            return NotFound();
            
        var reviews = await _toolService.GetToolReviewsAsync(id);
        return Ok(reviews);
    }
}