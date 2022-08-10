using Amazon.SQS;

namespace davek.dev.todo.Interfaces;

public interface ISqsClientFactory
{
    IAmazonSQS GetSqsClient();
    string GetSqsQueue();
}
