using System.Text.Json;
using Amazon.SQS.Model;
using davek.dev.todo.Interfaces;
using davek.dev.todo.Models;

namespace davek.dev.todo.Services;

public class SqsService : ISqsService
{
    private readonly ISqsClientFactory _sqsClientFactory;

    public SqsService(ISqsClientFactory sqsClientFactory)
    {
        _sqsClientFactory = sqsClientFactory;
    }

    public async Task<IEnumerable<ToDoItemModel>> GetToDoItemsAsync()
    {
        var messages = new List<ToDoItemModel>();

        var request = new ReceiveMessageRequest
        {
            QueueUrl = _sqsClientFactory.GetSqsQueue(),
            MaxNumberOfMessages = 10,
            VisibilityTimeout = 10,
            WaitTimeSeconds = 10,
        };

        var response = await _sqsClientFactory.GetSqsClient().ReceiveMessageAsync(request);

        foreach (var message in response.Messages)
        {
            try
            {
                var m = JsonSerializer.Deserialize<ToDoItemModel>(message.Body);
                if(m != null)
                    messages.Add(m);
            }
            catch
            {
                // Invalid message, ignore
            }
        }

        return messages;
    }

    public async Task PublishToDoItemAsync(ToDoItemModel item)
    {
        var request = new SendMessageRequest
        {
            MessageBody = JsonSerializer.Serialize(item),
            QueueUrl = _sqsClientFactory.GetSqsQueue(),
        };

         var client =  _sqsClientFactory.GetSqsClient();
         await client.SendMessageAsync(request);
    }
}
