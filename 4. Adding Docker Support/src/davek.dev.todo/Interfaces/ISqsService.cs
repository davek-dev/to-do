using davek.dev.todo.Models;

namespace davek.dev.todo.Services;

public interface ISqsService
{
    Task<IEnumerable<ToDoItemModel>> GetToDoItemsAsync();
    Task PublishToDoItemAsync(ToDoItemModel item);
}