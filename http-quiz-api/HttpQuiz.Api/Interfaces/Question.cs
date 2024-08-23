namespace HttpQuiz.Api.Interfaces;

public abstract class Question
{
    public abstract string Url { get; }

    public abstract HttpMethod Method { get; }

    public abstract QuestionBody? Body { get; init; }

    public abstract int Level { get; }

    public abstract Task<QuestionResult> Rank(HttpResponseMessage response);

    protected async Task<QuestionResult> Rank(HttpResponseMessage response, int answer)
    {
        if (!response.IsSuccessStatusCode)
        {
            return QuestionResult.FailedResponse;
        }

        var json = await response.Content.ReadAsStringAsync();

        if (int.TryParse(json, out var intValue))
        {
            if (intValue == answer)
            {
                return QuestionResult.Correct;
            }
        }

        if (json.Equals(answer.ToString(), StringComparison.InvariantCultureIgnoreCase))
        {
            return QuestionResult.CorrectButIncorrectDataType;
        }

        return QuestionResult.SuccessfulResponse;
    }

    protected async Task<QuestionResult> Rank(HttpResponseMessage response, string answer)
    {
        if (!response.IsSuccessStatusCode)
        {
            return QuestionResult.FailedResponse;
        }

        var json = await response.Content.ReadAsStringAsync();


        return json.Equals(answer, StringComparison.InvariantCultureIgnoreCase)
            ? QuestionResult.CorrectButIncorrectDataType
            : QuestionResult.SuccessfulResponse;
    }
}