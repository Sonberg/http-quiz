using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Logic;

public class GameState
{
    public DateTimeOffset? StartedAt { get; private set; }

    private Dictionary<Guid, int> Ranking { get; set; } = new();

    public Dictionary<Guid, Team> Teams { get; } = new();

    public int Level { get; set; } = 1;

    public int Delay { get; set; } = 30;

    public int Round { get; private set; }

    public ICollection<TeamScore> Leaderboard => Teams
        .Select(x => new TeamScore
        {
            TeamId = x.Value.Id,
            TeamName = x.Value.Name,
            Points = Ranking.GetValueOrDefault(x.Key, 0)
        })
        .OrderByDescending(x => x.Points)
        .ToList();

    public void Start()
    {
        StartedAt = DateTimeOffset.Now;
        Ranking = new Dictionary<Guid, int>();
    }

    public void Stop()
    {
        StartedAt = null;
        Delay = 30;
        Level = 0;
        Round = 0;
        Ranking.Clear();
    }

    public void AddTeam(Team team)
    {
        Ranking.Add(team.Id, 0);
        Teams.Add(team.Id, team);
    }

    public void RegisterRound(Dictionary<Guid, QuestionResult> questionResults)
    {
        Round += 1;

        foreach (var (id, result) in questionResults)
        {
            if (Ranking.ContainsKey(id))
            {
                Ranking[id] += (int)result;
            }
            else
            {
                Ranking.Add(id, (int)result);
            }
        }
    }
}