using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._5;

public class IsEven : Question
{
    public override string Url => $"is/even/{Input}";

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
        return new IsEven
        {
            Body = new QuestionBody
            {
                Question = "Is the number even?",
                Input = number
            },
            Input = number,
            Answer = (number % 2 == 0).ToString()
        };
    }
}