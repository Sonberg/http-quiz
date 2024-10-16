using HttpQuiz.Api.Hubs;

namespace HttpQuiz.Api.Logic;

public class GameRunner : BackgroundService
{
    private readonly GameState _state;

    private readonly GameEngine _engine;

    private readonly IServiceProvider _serviceProvider;

    public GameRunner(GameState state, GameEngine engine, IServiceProvider serviceProvider)
    {
        _state = state;
        _engine = engine;
        _serviceProvider = serviceProvider;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Task.Run(() => RunAsync(stoppingToken), stoppingToken);

        return Task.CompletedTask;
    }

    private async Task RunAsync(CancellationToken ct)
    {
        while (true)
        {
            if (_state.StartedAt is null)
            {
                await Task.Delay(TimeSpan.FromSeconds(2), ct);
                continue;
            }

            using var scope = _serviceProvider.CreateScope();
            var hub = scope.ServiceProvider.GetRequiredService<IGameHubContext>();

            Console.WriteLine("Starting next round");

            var question = _engine.GetQuestion(_state.Level);
            var users = _state.Teams.Select(x => x.Value).ToList();
            var delay = _state.Delay;

            Console.WriteLine($"Sending question: {question.Body?.Question ?? question.Url} to {users.Count} users");
            var results = await _engine.FireRound(question, users, ct);
            Console.WriteLine("We have a result");

            _state.RegisterRound(results);
            await hub.Update();

            Console.WriteLine($"Waiting for {delay} seconds");

            await hub.TimeLeft($"{delay}s");
            
            for (var i = 0; i < delay; i++)
            {
                if (_state.StartedAt is null)
                {
                    break;
                }

                await hub.TimeLeft($"{delay - i}s");
                await Task.Delay(1 * 1000, ct);
            }
            
            await hub.TimeLeft("-");
        }
    }
}