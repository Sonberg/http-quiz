namespace HttpQuiz.Api.Interfaces;

public record Team
{
    public Guid Id { get; init; } = Guid.NewGuid();
    
    public required string Name { get; set; }
    
    public required Uri BaseUrl { get; set; }
}