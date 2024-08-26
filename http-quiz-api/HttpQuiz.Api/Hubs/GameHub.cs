using Microsoft.AspNetCore.SignalR;

namespace HttpQuiz.Api.Hubs;

public class GameHub : Hub<IGameHubClient>
{
    private readonly IGameHubContext _hub;

    public GameHub(IGameHubContext hub)
    {
        _hub = hub;
    }

    public override Task OnConnectedAsync()
    {
        return _hub.Update();
    }
}