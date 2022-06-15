using davek.dev.todo.Models;

namespace davek.dev.todo.Interfaces;

public interface IToDoItemDataAccessService
{
    Task CreateItemAsync(ToDoItemModel model);
    Task<ToDoItemModel> GetToDoItemAsync(string id);
    Task<IEnumerable<ToDoItemModel>> GetOpenToDoItemsAsync();
    Task UpdateToDoItemAsync(ToDoItemModel model);
    Task DeleteToDoItemAsync(string id);
}
