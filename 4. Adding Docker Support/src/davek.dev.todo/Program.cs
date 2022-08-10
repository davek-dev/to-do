using davek.dev.todo.DataAccess;
using davek.dev.todo.Interfaces;
using davek.dev.todo.Jobs;
using davek.dev.todo.Models;
using davek.dev.todo.Services;
using davek.dev.todo.StartUp;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IJobs, Jobs>();
builder.Services.AddTransient<IToDoItemDataAccessService, ToDoItemDataAccessService>();
builder.Services.AddTransient<IToDoItemService, ToDoItemService>();

builder.Services.AddTransient<ISqsService, SqsService>();
builder.Services.AddSingleton<ISqsClientFactory, SqsClientFactory>();
builder.Services.Configure<SqsOptions>(builder.Configuration.GetSection("SqsOptions"));

builder.Services.AddSingleton<IMongoDbFactory>(new MongoDbFactory(builder.Configuration.GetValue<string>("ConnectionStrings:MongoDb")));

builder.ConfigureHangfireService();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard();

app.MapControllers();

app.ConfigureJobs();

app.Run();
