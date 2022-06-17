using System.Globalization;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace davek.dev.todo.Jobs;

public class Jobs : IJobs
{
    [Queue("default")]
    [JobDisplayName("Display current date and time")]
    public void DisplayCurrentDateTime(PerformContext context)
    {
        context.WriteLine($"The current date and time is: {DateTime.Now.ToString(CultureInfo.CurrentCulture)}");
        context.WriteLine($"The current UTC date and time is: {DateTime.UtcNow.ToString(CultureInfo.CurrentCulture)}");
    }
}
