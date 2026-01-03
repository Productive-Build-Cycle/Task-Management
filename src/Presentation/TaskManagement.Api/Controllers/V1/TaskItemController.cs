using Application.Services.Contracts.Task;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Application.Services.Dtos.TaskItemDto;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Request;
using TaskManagement.Application.Services.Dtos.TaskItemDto.Response;
using TaskManagement.Application.Wrapper;

namespace TaskManagement.Api.Controllers.V1;

public class TaskItemController(
    ITaskService taskService,
    IValidator<TaskItemCreateRequest> createValidator,
    IValidator<TaskItemUpdateRequest> updateValidator,
    IValidator<ChangeWorkFlowRequest> changeWorkFlowValidator,
    IValidator<ChangePriorityRequest> changePriorityValidator,
    IValidator<ReAssignRequest> reAssignRequestValidator
    ) : BaseController
{
    #region Get All

    [HttpGet]
    [ProducesResponseType(typeof(Result<List<TaskItemResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAll([FromQuery] TaskQueryableDto queryableDto)
    {
        var result = await taskService.GetAllAsync(queryableDto);

        if (result.IsSuccess is false)
            return BadRequest(result.Error);

        return Ok(result);
    }

    #endregion Get All

    #region Get By Id

    [HttpGet("{id:Guid}")]
    [ProducesResponseType(typeof(Result<TaskItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await taskService.GetByIdAsync(id, cancellationToken);

        if (result.IsSuccess is false)
            return NotFound(result.Error);

        return Ok(result);
    }

    #endregion Get By Id

    #region Create

    [HttpPost]
    [ProducesResponseType(typeof(Result<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] TaskItemCreateRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await createValidator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
            return BadRequest(validationResult.Errors.First().ErrorMessage);

        var result = await taskService.AddAsync(request, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.Value }, result.Value);
    }

    #endregion Create

    #region Update

    [HttpPut("{id:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] TaskItemUpdateRequest request, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await updateValidator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
            return BadRequest(validationResult.Errors.First().ErrorMessage);

        var result = await taskService.Update(request, id, cancellationToken);

        if (result.IsSuccess is false)
            return NotFound(result.Error);

        return NoContent();
    }

    #endregion Update

    #region Delete

    [HttpDelete("{id:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await taskService.Delete(id, cancellationToken);

        if (result.IsSuccess is false)
            return BadRequest(result.Error);

        return NoContent();
    }

    #endregion Delete

    #region Change WorkFlow

    [HttpPatch("change-workflow/{id:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeWorkFlow([FromBody] ChangeWorkFlowRequest changeWorkFlowRequest, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await changeWorkFlowValidator.ValidateAsync(changeWorkFlowRequest, cancellationToken);

        if (validationResult.IsValid is false)
            return BadRequest(validationResult.Errors.First().ErrorMessage);

        var result = await taskService.ChangeWorkFlow(changeWorkFlowRequest.workFlow, id, cancellationToken);

        if (result.IsSuccess is false)
            return NotFound(result.Error);

        return NoContent();
    }

    #endregion Change WorkFlow

    #region Change Priority

    [HttpPatch("change-priority/{id:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePriority([FromBody] ChangePriorityRequest changePriorityRequest, [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await changePriorityValidator.ValidateAsync(changePriorityRequest, cancellationToken);

        if (validationResult.IsValid is false)
            return BadRequest(validationResult.Errors.First().ErrorMessage);

        var result = await taskService.ChangePriority(changePriorityRequest.newPriority, id, cancellationToken);

        if (result.IsSuccess is false)
            return BadRequest(result.Error);

        return NoContent();
    }

    #endregion Change Priority

    #region ReAssign

    [HttpPatch("reassign/{id:Guid}")]
    [ProducesResponseType(typeof(Result), StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ReAssign([FromBody] ReAssignRequest request,
        [FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var validationResult = await reAssignRequestValidator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid is false)
            return BadRequest(validationResult.Errors.First().ErrorMessage);

        var result = await taskService.ReAssign(request.UserId, id, cancellationToken);

        if (result.IsSuccess is false)
            return BadRequest(result.Error);

        return NoContent();
    }
    

    #endregion
}