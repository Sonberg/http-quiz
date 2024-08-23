namespace HttpQuiz.Api.Interfaces;

public class QuestionBody
{
    public required string Question { get; set; }

    public required object Input { get; set; }
}