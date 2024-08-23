using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._2;

public class FindLowestNumber : Question
{
    public override string Url => "find/lowest-number";

    public override HttpMethod Method => HttpMethod.Post;

    public override QuestionBody? Body { get; init; }

    public override int Level => 2;

    public required int Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question From(int[] numbers)
    {
        return new FindLowestNumber
        {
            Answer = numbers.Min(),
            Body = new QuestionBody
            {
                Question = "Find the lowest value of a list with numbers",
                Input = numbers,
            }
        };
    }
}