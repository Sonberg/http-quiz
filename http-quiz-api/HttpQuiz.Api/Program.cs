using System.Text.Json;
using System.Text.Json.Serialization;
using HttpQuiz.Api;
using HttpQuiz.Api.Hubs;
using HttpQuiz.Api.Interfaces;
using HttpQuiz.Api.Logic;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<GameState>();
builder.Services.AddSingleton<GameEngine>();
builder.Services.AddHostedService<GameRunner>();
builder.Services.AddScoped<IGameHubContext, GameHubContext>();
builder.Services.AddHttpClient("Gun", opt => { opt.Timeout = TimeSpan.FromSeconds(10); });
builder.Services
    .AddSignalR()
    .AddJsonProtocol(opt =>
    {
        opt.PayloadSerializerOptions.PropertyNameCaseInsensitive = true;
        opt.PayloadSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        opt.PayloadSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .SetIsOriginAllowed((_) => true)
            .Build();
    });
});


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors();
app.MapHub<GameHub>("/hub");
app.UseHttpsRedirection();

app.MapPost("/api/add-team", async ([FromBody] NewUser user, GameState state, IGameHubContext hub) =>
    {
        state.AddTeam(new Team
        {
            Id = Guid.NewGuid(),
            Name = user.Name,
            BaseUrl = user.BaseUrl
        });
        await hub.Update();
    })
    .WithName("AddTeam")
    .WithOpenApi();

app.MapPost("/api/start", async (GameState state, IGameHubContext hub) =>
    {
        state.Start();
        await hub.Update();
    })
    .WithName("StartGame")
    .WithOpenApi();

app.MapPost("/api/stop", async (GameState state, IGameHubContext hub) =>
    {
        state.Stop();
        await hub.Update();
        await hub.TimeLeft("-");
    })
    .WithName("StopGame")
    .WithOpenApi();

app.MapPost("/api/clear", async (GameState state, IGameHubContext hub) =>
{
    state.Clear();
    await hub.Update();
    await hub.TimeLeft("-");
})
.WithName("ResetGame")
.WithOpenApi();

app.MapPut("/api/set-delay", async ([FromBody] SetDelay body, GameState state, IGameHubContext hub) =>
    {
        state.Delay = body.Delay;
        await hub.Update();
    })
    .WithName("SetDelay")
    .WithOpenApi();

app.MapPut("/api/set-level", async ([FromBody] SetLevel body, GameState state, IGameHubContext hub) =>
    {
        state.Level = body.Level;
        await hub.Update();
    })
    .WithName("SetLevel")
    .WithOpenApi();


app.MapGet("/api/leaderboard", (GameState state) => state.Leaderboard)
    .WithName("GetLeaderboard")
    .WithOpenApi();

app.MapGet("/api/stats", (GameState state) => new Stats
{
    IsStarted = state.StartedAt is not null,
    Delay = state.Delay,
    Round = state.Round,
    Level = state.Level,
    Levels = GameConstants.Questions.Select(x => x.Level).Distinct().Order().ToArray()
})
    .WithName("GetStats")
    .WithOpenApi();


app.Run();

namespace HttpQuiz.Api
{
    public record NewUser
    {
        public required string Name { get; init; }

        public required Uri BaseUrl { get; init; }
    }

    public record SetDelay
    {
        public required int Delay { get; init; }
    }

    public record SetLevel
    {
        public required int Level { get; init; }
    }


    public record Stats
    {
        public required bool IsStarted { get; init; }

        public required int Round { get; init; }

        public required int Level { get; init; }

        public required int Delay { get; init; }

        public required int[] Levels { get; init; }
    }
}