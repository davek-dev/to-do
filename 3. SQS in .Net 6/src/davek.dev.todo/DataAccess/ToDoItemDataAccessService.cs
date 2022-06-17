using davek.dev.todo.Interfaces;
using davek.dev.todo.Models;
using MongoDB.Driver;

namespace davek.dev.todo.DataAccess;

public class ToDoItemDataAccessService : IToDoItemDataAccessService
{
    private readonly IMongoCollection<ToDoItemModel> _toDoItems;

    public ToDoItemDataAccessService(IMongoDbFactory mongoDbFactory)
    {
        _toDoItems = mongoDbFactory.GetCollection<ToDoItemModel>("ToDo", "ToDoItems");
    }

    public async Task CreateItemAsync(ToDoItemModel model)
    {
        await _toDoItems.InsertOneAsync(model);
    }

    public async Task<ToDoItemModel> GetToDoItemAsync(string id)
    {
        var results = await _toDoItems.FindAsync(x => x.Id == id);
        return await results.SingleAsync();
    }

    public async Task<IEnumerable<ToDoItemModel>> GetOpenToDoItemsAsync()
    {
        var results = await _toDoItems.FindAsync(x => !x.Complete);
        return await results.ToListAsync();
    }

    public async Task UpdateToDoItemAsync(ToDoItemModel model)
    {
        await _toDoItems.FindOneAndReplaceAsync(x => x.Id == model.Id, model);
    }

    public async Task DeleteToDoItemAsync(string id)
    {
        await _toDoItems.DeleteOneAsync(x => x.Id == id);
    }
}
