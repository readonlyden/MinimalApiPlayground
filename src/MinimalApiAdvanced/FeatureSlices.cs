namespace MinimalApiAdvanced;

public interface IRequestHandler<in TRequest>
{
    Task<IResult> HandleRequest(
        TRequest request, CancellationToken ct);
}

public static class RequestHandlerExtensions
{
    public static RouteHandlerBuilder MapPostHandler<TRequest, THandler>(
        this IEndpointRouteBuilder builder,
        string pattern
    ) 
        where THandler: IRequestHandler<TRequest>
    {
        return builder.MapPost(pattern, async (
            TRequest request,
            THandler handler,
            CancellationToken ct
        ) =>
        {
            await handler.HandleRequest(request, ct);
        });
    }
}

