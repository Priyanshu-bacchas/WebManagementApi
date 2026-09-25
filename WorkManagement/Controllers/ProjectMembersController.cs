using Microsoft.AspNetCore.Mvc;
using WorkManagement.DTOs.ProjectMember;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectMembersController : ControllerBase
{
    private readonly IProjectMemberService _service;

    public ProjectMembersController(
        IProjectMemberService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var member =
            await _service.GetByIdAsync(id);

        if (member == null)
            return NotFound("Project member not found.");

        return Ok(member);
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetByProject(
        int projectId)
    {
        var members =
            await _service.GetByProjectAsync(projectId);

        return Ok(members);
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetByUser(
        int userId)
    {
        var members =
            await _service.GetByUserAsync(userId);

        return Ok(members);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        ProjectMemberCreateDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        ProjectMemberUpdateDto dto)
    {
        var result =
            await _service.UpdateAsync(id, dto);

        if (result == null)
            return NotFound("Project member not found.");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _service.DeleteAsync(id);

        if (!result)
            return NotFound("Project member not found.");

        return Ok("Project member deleted successfully.");
    }
}