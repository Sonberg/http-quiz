using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._1;

public class MeaningOfLife : Question
{
    public override string Url => "meaning-of-life";
    
    public override HttpMethod Method => HttpMethod.Get;
    
    public override QuestionBody? Body { get; init; }
    
    public override int Level => 1;

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return base.Rank(response, 42);
    }

    public static Question New()
    {
        return new MeaningOfLife
        {
            Body = new QuestionBody
            {
                Question = "Have you read The hitchhiker's guide to the galaxy?",
                Input = new {}
            }
        };
    }
}