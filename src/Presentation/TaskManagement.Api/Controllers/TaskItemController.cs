namespace Api.Controllers;

using Application.Services.Contracts.Task;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Domain.Enum;
using Application.Services.Dtos.TaskItemDto;

[ApiController]
[Route("api/v1/[controller]")]
public class TaskItemController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskItemController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetTaskItem([FromRoute] Guid id)
    {
        var res =  await _taskService.GetByIdAsync(id);

        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        
        return Ok(res.Value);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] TaskItemCreateDto request)
    {
       var res = await _taskService.AddAsync(request);
       if (!res.IsSuccess)
       {
           return BadRequest(res.Error);
       }
       return Created("", res);
    }

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromBody] TaskItemUpdateDto request,[FromRoute] Guid id)
    {
        var res = await _taskService.Update(request, id);
        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        
        return NoContent();
    }

    [HttpPatch("change-workflow/{id:Guid}")]
    [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status204NoContent)]
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
    [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(typeof(Task<IActionResult>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Delete([FromRoute]Guid id)
    {
        var res = await _taskService.Delete(id);
        
        if (!res.IsSuccess)
        {
            return BadRequest(res.Error);
        }
        
        return NoContent();
    }
}