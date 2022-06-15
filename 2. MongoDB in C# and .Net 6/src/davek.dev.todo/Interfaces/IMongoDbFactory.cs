using MongoDB.Driver;

namespace davek.dev.todo.Interfaces;

public interface IMongoDbFactory
{
    IMongoCollection<T> GetCollection<T>(string databaseName, string collectionNme);
}
