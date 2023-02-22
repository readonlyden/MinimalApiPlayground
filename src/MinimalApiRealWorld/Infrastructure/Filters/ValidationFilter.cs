using FluentValidation;

namespace MinimalApiRealWorld.Infrastructure.Filters;

public class ValidationFilter<T> : IEndpointFilter
    where T : class
{
    private readonly IValidator<T>? _validator;

    public ValidationFilter(IValidator<T>? validator = null)
    {
        _validator = validator;
    }

    public async ValueTask<object?> InvokeAsync(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        if (_validator is null)
        {
            return await next(context);
        }
        
        var argumentToValidate = context.Arguments
            .SingleOrDefault(x => x?.GetType() == typeof(T)) as T;

        if (argumentToValidate is null)
        {
            return Results.BadRequest();
        }

        var validationResult = await _validator
            .ValidateAsync(argumentToValidate);

        if (!validationResult.IsValid)
        {
            return Results.BadRequest();
        }

        return await next(context);
    }
}