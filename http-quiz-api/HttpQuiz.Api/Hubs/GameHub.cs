using Microsoft.AspNetCore.SignalR;

namespace HttpQuiz.Api.Hubs;

public interface IGameHubClient
{
    Task Reload();

    Task TimeLeft(string value);
}

public interface IGameHubContext : IGameHubClient
{
}

public class GameHubContext : IGameHubContext
{
    private readonly IHubContext<GameHub, IGameHubClient> _hub;

    public GameHubContext(IHubContext<GameHub, IGameHubClient> hub)
    {
        _hub = hub;
    }

    public Task Reload()
    {
        return _hub.Clients.All.Reload();
    }

    public Task TimeLeft(string value)
    {
        return _hub.Clients.All.TimeLeft(value);
    }
}

public class GameHub : Hub<IGameHubClient>
{
}