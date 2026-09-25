using Microsoft.AspNetCore.Mvc;
using WorkManagement.DTOs.Project;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _service;

    public ProjectsController(IProjectService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var projects = await _service.GetAllAsync();

        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var project = await _service.GetByIdAsync(id);

        if (project == null)
            return NotFound("Project not found.");

        return Ok(project);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(int userId)
    {
        var projects =
            await _service.GetByUserAsync(userId);

        return Ok(projects);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProjectCreateDto dto)
    {
        var result = await _service.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        ProjectUpdateDto dto)
    {
        var result =
            await _service.UpdateAsync(id, dto);

        if (result == null)
            return NotFound("Project not found.");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _service.DeleteAsync(id);

        if (!result)
            return NotFound("Project not found.");

        return Ok("Project deleted successfully.");
    }
}