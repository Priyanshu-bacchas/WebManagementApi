using Microsoft.AspNetCore.Mvc;
using WorkManagement.DTOs.WorkItem;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WorkItemsController : ControllerBase
{
    private readonly IWorkItemService _service;

    public WorkItemsController(IWorkItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var workItems =
            await _service.GetAllAsync();

        return Ok(workItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var workItem =
            await _service.GetByIdAsync(id);

        if (workItem == null)
            return NotFound("Work item not found.");

        return Ok(workItem);
    }

    [HttpGet("project/{projectId}")]
    public async Task<IActionResult> GetByProject(
        int projectId)
    {
        var workItems =
            await _service.GetByProjectAsync(projectId);

        return Ok(workItems);
    }

    [HttpGet("assignee/{userId}")]
    public async Task<IActionResult> GetByAssignee(
        int userId)
    {
        var workItems =
            await _service.GetByAssigneeAsync(userId);

        return Ok(workItems);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        WorkItemCreateDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        WorkItemUpdateDto dto)
    {
        var result =
            await _service.UpdateAsync(id, dto);

        if (result == null)
            return NotFound("Work item not found.");

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _service.DeleteAsync(id);

        if (!result)
            return NotFound("Work item not found.");

        return Ok("Work item deleted successfully.");
    }
}