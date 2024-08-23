using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._4;

public class ContainsSubstring : Question
{
    public override string Url => "contains/substring";

    public override HttpMethod Method => HttpMethod.Put;

    public override QuestionBody? Body { get; init; }

    public override int Level => 4;

    public required string Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question From(string sentence, string substring)
    {
        return new ContainsSubstring
        {
            Body = new QuestionBody
            {
                Question = "Check if the sentence contains a substring",
                Input = new
                {
                    sentence,
                    substring
                }
            },
            Answer = sentence.Contains(substring, StringComparison.InvariantCultureIgnoreCase).ToString()
        };
    }
}