namespace Api.Controllers;

using Application.Services.Contracts.Task;
using Application.Services.Dtos;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Enum;

[ApiController]
[Route("api/v1/task")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskItemController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTaskItem()
    {
        var res =  await _taskService.GetAllAsync();

        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        return Ok(res.Value);
    }
    
    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetTaskItem(Guid id)
    {
        var res =  await _taskService.GetByIdAsync(id);

        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        
        return Ok(res.Value);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTaskItem([FromBody] TaskCreateDto request)
    {
       var res = await _taskService.AddAsync(request);
       if (!res.IsSuccess)
       {
           return BadRequest(res.Error);
       }
       return Created("", res.Value?.GuidRow);
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateTaskItem([FromBody] TaskUpdateDto request,[FromRoute] Guid id)
    {
        var res = await _taskService.Update(request, id);
        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        
        return NoContent();
    }

    [HttpPatch("change-workflow/{id:Guid}")]
    public async Task<IActionResult> ChangeWorkFlowTaskItem([FromBody] WorkFlow workFlow, [FromRoute] Guid id)
    {
        Console.WriteLine(workFlow);
        var res = await _taskService.ChangeWorkFlow(workFlow, id);
        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        return NoContent();
    }
    
    [HttpPatch("change-priority/{id:Guid}")]
    public async Task<IActionResult> ChangePriorityTaskItem([FromBody] Priority newPriority, [FromRoute] Guid id)
    {
        var res = await _taskService.ChangePriority(newPriority, id);
        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        return NoContent();
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteTaskItem([FromRoute]Guid id)
    {
        var res = await _taskService.Delete(id);
        
        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        
        return NoContent();
    }
}