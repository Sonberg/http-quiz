using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._3;

public class CurrentYear : Question
{
    public override string Url => "current/year";

    public override HttpMethod Method => HttpMethod.Get;

    public override QuestionBody? Body { get; init; }

    public override int Level => 3;

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return base.Rank(response, DateTimeOffset.Now.Year);
    }

    public static Question New()
    {
        return new CurrentYear();
    }
}