using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._4;

public class CalculatePercentage : Question
{
    public override string Url => "calculate/percentage";

    public override HttpMethod Method => HttpMethod.Post;

    public override QuestionBody? Body { get; init; }

    public override int Level => 4;

    public required int Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question From(int number, int total)
    {
        return new CalculatePercentage
        {
            Answer = number / total * 100,
            Body = new QuestionBody
            {
                Question = "Calculate percentage",
                Input = new
                {
                    Number = number,
                    Total = total
                }
            }
        };
    }
}