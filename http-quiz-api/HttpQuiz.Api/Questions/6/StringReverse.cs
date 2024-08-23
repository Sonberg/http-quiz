using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._6;

public class StringReverse : Question
{
    public override string Url => "string/reverse";

    public override HttpMethod Method => HttpMethod.Post;

    public override QuestionBody? Body { get; init; }

    public override int Level => 6;


    public required string Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question From(string input)
    {
        return new StringReverse
        {
            Answer = Reverse(input),
            Body = new QuestionBody
            {
                Question = "Return reversed string",
                Input = input
            }
        };
    }

    private static string Reverse(string input)
    {
        ArgumentNullException.ThrowIfNull(input);

        var charArray = input.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}