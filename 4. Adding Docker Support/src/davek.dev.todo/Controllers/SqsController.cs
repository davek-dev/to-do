using davek.dev.todo.Interfaces;
using davek.dev.todo.Models;
using davek.dev.todo.Services;
using Microsoft.AspNetCore.Mvc;

namespace davek.dev.todo.Controllers;

[Route("[controller]")]
public class SqsController : ControllerBase
{
    private readonly ISqsService _sqsService;
    private readonly IToDoItemService _toDoItemService;

    public SqsController(ISqsService sqsService, IToDoItemService toDoItemService)
    {
        _sqsService = sqsService;
        _toDoItemService = toDoItemService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> PublishToDoItem([FromBody] ToDoItemModel model)
    {
        try
        {
            await _sqsService.PublishToDoItemAsync(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Retrieve")]
    public async Task<IActionResult> RetrieveToDoItems()
    {
        try
        {
            var items = await _sqsService.GetToDoItemsAsync();

            foreach (var item in items)
            {
                await _toDoItemService.CreateItemAsync(item);
            }

            return Ok(items.Count());
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
