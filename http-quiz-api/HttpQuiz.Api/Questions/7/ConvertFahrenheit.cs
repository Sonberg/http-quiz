using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._7;

public class ConvertFahrenheit : Question
{
    public override string Url => "convert/fahrenheit";

    public override HttpMethod Method => HttpMethod.Post;

    public override QuestionBody? Body { get; init; }

    public override int Level => 7;
    
    public required int Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question New()
    {
        return new ConvertFahrenheit
        {
            Answer = 32,
            Body = new QuestionBody
            {
                Question = "Convert the temperature from Celsius to Fahrenheit.",
                Input = 0
            }
        };
    }
}