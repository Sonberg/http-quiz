using HttpQuiz.Api.Interfaces;

namespace HttpQuiz.Api.Questions._7;

public class FindMedian : Question
{
    public override string Url => "find/median";

    public override HttpMethod Method => HttpMethod.Post;

    public override QuestionBody? Body { get; init; }

    public override int Level => 7;

    public required int Answer { get; init; }

    public override Task<QuestionResult> Rank(HttpResponseMessage response)
    {
        return Rank(response, Answer);
    }

    public static Question From(int[] numbers)
    {
        return new FindMedian
        {
            Answer = FindMedianValue(numbers),
            Body = new QuestionBody
            {
                Question = "Return the median value from the list of numbers",
                Input = numbers
            }
        };
    }

    private static int FindMedianValue(int[] numbers)
    {
        var middleIndex = Convert.ToInt32(Math.Floor(numbers.Length / (decimal)2));

        if (numbers.Length % 2 == 0)
        {
            return (numbers[middleIndex - 1] + numbers[middleIndex]) / 2;
        }

        return numbers[middleIndex];
    }
}