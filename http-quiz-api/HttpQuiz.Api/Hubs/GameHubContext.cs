using HttpQuiz.Api.Logic;
using Microsoft.AspNetCore.SignalR;

namespace HttpQuiz.Api.Hubs;

public class GameHubContext : IGameHubContext
{
    private readonly IHubContext<GameHub, IGameHubClient> _hub;

    private readonly GameState _state;

    public GameHubContext(IHubContext<GameHub, IGameHubClient> hub, GameState state)
    {
        _hub = hub;
        _state = state;
    }

    public Task Reload()
    {
        return _hub.Clients.All.Reload();
    }

    public Task TimeLeft(string value)
    {
        return _hub.Clients.All.TimeLeft(value);
    }

    public Task Update()
    {
        return _hub.Clients.All.State(new GameCurrentState
        {
            Leaderboard = _state.Leaderboard,
            IsStarted = _state.StartedAt is not null,
            Delay = _state.Delay,
            Round = _state.Round,
            Level = _state.Level,
            Levels = GameConstants.Questions.Select(x => x.Level).Distinct().Order().ToArray()
        });
    }
}