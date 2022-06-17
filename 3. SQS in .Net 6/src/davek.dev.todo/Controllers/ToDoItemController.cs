using davek.dev.todo.Interfaces;
using davek.dev.todo.Models;
using Microsoft.AspNetCore.Mvc;

namespace davek.dev.todo.Controllers;

[Route("[controller]")]
public class ToDoItemController : ControllerBase
{
    private readonly IToDoItemService _toDoItemService;

    public ToDoItemController(IToDoItemService toDoItemService)
    {
        _toDoItemService = toDoItemService;
    }

    [HttpPost("Create")]
    public async Task<IActionResult> CreateToDoItem([FromBody] ToDoItemModel model)
    {
        try
        {
            await _toDoItemService.CreateItemAsync(model);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("Get")]
    public async Task<IActionResult> GetToDoItem([FromQuery]string id)
    {
        try
        {
            var item = await _toDoItemService.GetToDoItemAsync(id);
            return Ok(item);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("GetOpenItems")]
    public async Task<IActionResult> GetOpenItems()
    {
        try
        {
            var items = await _toDoItemService.GetOpenToDoItemsAsync();
            return Ok(items);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
