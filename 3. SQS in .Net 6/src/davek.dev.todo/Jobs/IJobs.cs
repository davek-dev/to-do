using Hangfire.Server;

namespace davek.dev.todo.Jobs;

public interface IJobs
{
    void DisplayCurrentDateTime(PerformContext context);
}
