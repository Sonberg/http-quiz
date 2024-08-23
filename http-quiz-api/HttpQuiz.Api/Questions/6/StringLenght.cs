using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._6;

public class StringLenght : Question
{
    public override string Url => "string/length";

    public override HttpMethod Method => HttpMethod.Post;

    public override QuestionBody? Body { get; init; }

    public override int Level => 6;

    public required int Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static StringLenght From(string input)
    {
        return new StringLenght
        {
            Answer = input.Length,
            Body = new QuestionBody
            {
                Question = "How long is the length of the string?",
                Input = input
            }
        };
    }
}