using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._5;

public class IsOdd : Question
{
    public override string Url => $"is/odd/{Input}";

    public override HttpMethod Method => HttpMethod.Get;

    public override QuestionBody? Body { get; init; }

    public override int Level => 5;

    public required int Input { get; init; }

    public required string Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question From(int number)
    {
        return new IsOdd
        {
            Body = new QuestionBody
            {
                Question = "Is the number odd?",
                Input = number
            },
            Input = number,
            Answer = (number % 2 == 1).ToString()
        };
    }
}