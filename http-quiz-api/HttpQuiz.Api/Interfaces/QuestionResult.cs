namespace HttpQuiz.Api.Interfaces;

public enum QuestionResult
{
    Correct = 10,
    CorrectButIncorrectDataType = 5,
    SuccessfulResponse = 2,
    FailedResponse = 0,
    Error = -1,
}