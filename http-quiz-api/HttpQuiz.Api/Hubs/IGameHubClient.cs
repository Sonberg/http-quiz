namespace HttpQuiz.Api.Hubs;

public interface IGameHubClient
{
    Task Reload();

    Task TimeLeft(string value);

    Task State(GameCurrentState state);
}