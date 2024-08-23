namespace HttpQuiz.Api.Interfaces;

public class TeamScore
{
    public required Guid TeamId { get; set; }
    
    public required string TeamName { get; set; }
    
    public required int Points { get; set; }
}