using System.Collections.Concurrent;
using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Logic;

public class GameEngine
{
    private static readonly Random Random = new();

    private readonly IHttpClientFactory _httpClientFactory;

    public GameEngine(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public Question GetQuestion(int level)
    {
        var questions = GameConstants.Questions.Where(x => x.Level <= level).ToArray();
        var index = Random.Next(0, questions.Length - 1);

        return questions[index];
    }

    public async Task<Dictionary<Guid, QuestionResult>> FireRound(
        Question question,
        ICollection<Team> users,
        CancellationToken ct)
    {
        var result = new ConcurrentDictionary<Guid, QuestionResult>();

        await Parallel.ForEachAsync(users, ct, async (user, _) =>
        {
            try
            {
                var client = _httpClientFactory.CreateClient("Gun");
                var request = new HttpRequestMessage(question.Method, question.Url);

                client.BaseAddress = user.BaseUrl;
                request.Content = question.Body != null
                    ? JsonContent.Create(question.Body)
                    : null;

                var response = await client.SendAsync(request, ct);
                var rank = await question.Rank(response);

                result.TryAdd(user.Id, rank);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                result.TryAdd(user.Id, QuestionResult.Error);
            }
        });

        return result.ToDictionary(x => x.Key, x => x.Value);
    }
}