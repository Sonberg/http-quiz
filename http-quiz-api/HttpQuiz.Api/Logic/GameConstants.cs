using HttpQuiz.Api.Interfaces;
using HttpQuiz.Api.Questions;
using HttpQuiz.Api.Questions._1;
using HttpQuiz.Api.Questions._2;
using HttpQuiz.Api.Questions._3;
using HttpQuiz.Api.Questions._4;
using HttpQuiz.Api.Questions._5;
using HttpQuiz.Api.Questions._6;
using HttpQuiz.Api.Questions._7;
using HttpQuiz.Api.Questions._8;

namespace HttpQuiz.Api.Logic;

public static class GameConstants
{
    public static readonly ICollection<Question> Questions =
    [
        Addition.From(1, 1, 2),
        Addition.From(10, 10, 20),
        Addition.From(100, 100, 200),
        MeaningOfLife.New(),
        FindHighestNumber.From([1, 2, 4, 5, 6, 7, 8, 9, 10, 11, 12]),
        FindHighestNumber.From([100, 200, 300]),
        FindLowestNumber.From([4, 6, 2, 7, 3, 89]),
        FindLowestNumber.From([34, 2, 3, 4, 5, 6]),
        CurrentYear.New(),
        RoundToNearestInteger.From(3.8m),
        RoundToNearestInteger.From(3.14m),
        RoundToNearestInteger.From(5.5m),
        CalculatePercentage.From(50, 200),
        CalculatePercentage.From(52, 3455),
        ContainsSubstring.From("I love H&M", "love"),
        ContainsSubstring.From("Assortment Demand Planning", "Marievik"),
        IsEven.From(20),
        IsEven.From(23),
        IsOdd.From(100),
        IsOdd.From(200),
        IsOdd.From(201),
        StringLenght.From("I love H&M"),
        StringLenght.From("ASQ, Squid & APT"),
        StringReverse.From("Assortment Demand Planning"),
        ConvertFahrenheit.New(),
        FindMedian.From([1, 2, 3, 4, 5]),
        FindMedian.From([2, 4, 6, 8]),
        ConvertToLowerCase.From("ChennAi"),
        ConvertToLowerCase.From("Sweden"),
        ConvertToUpperCase.From("Stockholm"),
        ConvertToUpperCase.From("Bangalore"),
    ];
}