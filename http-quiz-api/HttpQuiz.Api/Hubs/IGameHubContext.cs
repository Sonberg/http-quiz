namespace HttpQuiz.Api.Hubs;

public interface IGameHubContext
{
    Task TimeLeft(string value);

    Task Update();
}