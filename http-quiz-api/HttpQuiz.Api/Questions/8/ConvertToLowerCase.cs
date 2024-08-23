using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._8;

public class ConvertToLowerCase : Question
{
    public override string Url => "convert/lower-case";

    public override HttpMethod Method => HttpMethod.Post;

    public override QuestionBody? Body { get; init; }

    public override int Level => 8;

    public required string Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question From(string input)
    {
        return new ConvertToLowerCase
        {
            Answer = input.ToLower(),
            Body = new QuestionBody
            {
                Question = "Convert to lower case",
                Input = input
            }
        };
    }
}