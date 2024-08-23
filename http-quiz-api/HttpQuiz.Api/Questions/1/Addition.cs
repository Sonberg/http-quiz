using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._1;

public class Addition : Question
{
    public override string Url => "calculate/addition";

    public override HttpMethod Method => HttpMethod.Post;

    public override required QuestionBody? Body { get; init; }

    public required int Answer { get; set; }

    public override int Level => 1;

    public override async Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return await Rank(response, Answer);
    }

    public static Question From(int num1, int num2, int answer)
    {
        return new Addition
        {
            Answer = answer,
            Body = new QuestionBody
            {
                Question = "What is the sum of two numbers?",
                Input = new
                {
                    num1,
                    num2
                },
            }
        };
    }
}