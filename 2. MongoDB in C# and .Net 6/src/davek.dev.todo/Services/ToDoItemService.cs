using davek.dev.todo.Interfaces;
using davek.dev.todo.Models;

namespace davek.dev.todo.Services;

public class ToDoItemService : IToDoItemService
{
    private readonly IToDoItemDataAccessService _toDoItemDataAccessService;

    public ToDoItemService(IToDoItemDataAccessService toDoItemDataAccessService)
    {
        _toDoItemDataAccessService = toDoItemDataAccessService;
    }

    public async Task CreateItemAsync(ToDoItemModel model)
    {
        await _toDoItemDataAccessService.CreateItemAsync(model);
    }

    public async Task<ToDoItemModel> GetToDoItemAsync(string id)
    {
        return await _toDoItemDataAccessService.GetToDoItemAsync(id);
    }

    public async Task<IEnumerable<ToDoItemModel>> GetOpenToDoItemsAsync()
    {
        return await _toDoItemDataAccessService.GetOpenToDoItemsAsync();
    }

    public async Task UpdateToDoItemAsync(ToDoItemModel model)
    {
        await _toDoItemDataAccessService.UpdateToDoItemAsync(model);
    }

    public async Task DeleteToDoItemAsync(string id)
    {
        await _toDoItemDataAccessService.DeleteToDoItemAsync(id);
    }
}
