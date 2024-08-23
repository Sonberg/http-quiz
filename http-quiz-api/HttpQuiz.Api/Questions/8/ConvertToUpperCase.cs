using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._8;

public class ConvertToUpperCase : Question
{
    public override string Url => "convert/upper-case";

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
        return new ConvertToUpperCase()
        {
            Answer = input.ToUpper(),
            Body = new QuestionBody
            {
                Question = "Convert to uppser case",
                Input = input
            }
        };
    }
}