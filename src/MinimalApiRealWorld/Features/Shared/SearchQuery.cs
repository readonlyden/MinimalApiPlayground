namespace MinimalApiRealWorld.Features.Shared;

public class SearchQuery
{
    public int Page { get; set; } = 1;
    public int PerPage { get; set; } = 20;
    
    public string? SearchBy { get; set; }
}