using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace davek.dev.todo.Models;

public class ToDoItemModel
{
    public string Id { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateCompleted { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public ToDoItemNoteModel[] Notes { get; set; }

    public bool Complete { get; set; }
}
