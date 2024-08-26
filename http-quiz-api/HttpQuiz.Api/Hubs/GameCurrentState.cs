using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Hubs;

public record GameCurrentState
{
    public required ICollection<TeamScore> Leaderboard { get; init; }

    public required bool IsStarted { get; init; }

    public required int Round { get; init; }

    public required int Level { get; init; }

    public required int Delay { get; init; }

    public required int[] Levels { get; init; }
}