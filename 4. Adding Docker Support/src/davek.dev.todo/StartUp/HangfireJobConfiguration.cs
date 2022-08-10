using davek.dev.todo.Jobs;
using Hangfire;

namespace davek.dev.todo.StartUp;

public static class HangfireJobConfiguration
{
    public static void ConfigureJobs(this WebApplication app)
    {
        app.Services.CreateScope();

        RecurringJob.AddOrUpdate(() => app.Services.GetService<IJobs>().DisplayCurrentDateTime(null),
            "*/5 * * * *");
    }
}
