using davek.dev.todo.Jobs;
using Hangfire;
using Hangfire.Console;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;
using MongoDB.Driver;

namespace davek.dev.todo.StartUp;

public static class HangfireService
{
    public static void ConfigureHangfireService(this WebApplicationBuilder builder)
    {
        var mongoUrlBuilder = new MongoUrlBuilder(builder.Configuration.GetValue<string>("ConnectionStrings:MongoDb"));
        var mongoClient = new MongoClient(mongoUrlBuilder.ToMongoUrl());

        builder.Services.AddHangfire(configuration => configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseConsole()
            .UseMongoStorage(mongoClient, mongoUrlBuilder.DatabaseName, new MongoStorageOptions
            {
                MigrationOptions = new MongoMigrationOptions
                {
                    MigrationStrategy = new MigrateMongoMigrationStrategy(),
                    BackupStrategy = new CollectionMongoBackupStrategy()
                },
                Prefix = "todo.hangfire",
                CheckConnection = false
            })
        );

        builder.Services.AddHangfireServer(serverOptions =>
        {
            serverOptions.ServerName = "ToDo.Hangfire";
        });
    }
}
