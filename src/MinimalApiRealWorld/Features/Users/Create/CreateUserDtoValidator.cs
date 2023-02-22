using FluentValidation;

namespace MinimalApiRealWorld.Features.Users.Create;

public class CreateUserDtoValidator: AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(d => d.Email)
            .NotEmpty()
            .EmailAddress();

        RuleFor(d => d.FirstName).NotEmpty();
        RuleFor(d => d.LastName).NotEmpty();
    }
}