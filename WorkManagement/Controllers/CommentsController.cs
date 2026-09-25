using Microsoft.AspNetCore.Mvc;
using WorkManagement.DTOs.Comment;
using WorkManagement.Services.Interfaces;

namespace WorkManagement.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _service;

    public CommentsController(ICommentService service)
    {
        _service = service;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var comment =
            await _service.GetByIdAsync(id);

        if (comment == null)
            return NotFound("Comment not found.");

        return Ok(comment);
    }

    [HttpGet("workitem/{workItemId}")]
    public async Task<IActionResult> GetByWorkItem(
        int workItemId)
    {
        var comments =
            await _service.GetByWorkItemAsync(workItemId);

        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CommentCreateDto dto)
    {
        var result =
            await _service.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        CommentUpdateDto dto)
    {
        var result =
            await _service.UpdateAsync(id, dto);

        if (result == null)
            return NotFound("Comment not found.");

        return Ok(result);
    }
    /*
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _service.DeleteAsync(id);

        if (!result)
            return NotFound("Comment not found.");

        return Ok("Comment deleted successfully.");
    }
    */
}