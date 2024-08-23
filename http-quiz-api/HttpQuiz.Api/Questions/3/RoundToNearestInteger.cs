using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._3;

public class RoundToNearestInteger : Question
{
    public override string Url => "round/nearest-integer";

    public override HttpMethod Method => HttpMethod.Post;

    public override QuestionBody? Body { get; init; }

    public override int Level => 3;

    public required int Answer { get; set; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question From(decimal number)
    {
        return new RoundToNearestInteger
        {
            Answer = Convert.ToInt32(Math.Round(number)),
            Body = new QuestionBody
            {
                Question = "Round a number to the nearest integer",
                Input = number
            }
        };
    }
}