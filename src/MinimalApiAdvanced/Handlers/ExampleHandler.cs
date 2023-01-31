namespace MinimalApiAdvanced.Handlers;

public class ExampleRequest
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class ExampleHandler: IRequestHandler<ExampleRequest>
{
    private readonly ILogger<ExampleHandler> _logger;

    public ExampleHandler(ILogger<ExampleHandler> logger)
    {
        _logger = logger;
    }
    
    public async Task<IResult> HandleRequest(ExampleRequest request, CancellationToken ct)
    {
        _logger.LogInformation("Received request");
        await Task.Delay(1000, ct);
        _logger.LogInformation("Processed request");

        return Results.Ok();
    }
}