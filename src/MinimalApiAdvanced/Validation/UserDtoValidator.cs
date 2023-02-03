using FluentValidation;

namespace MinimalApiAdvanced.Validation;

public class UserDtoValidator: AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(m => m.Name)
            .NotEmpty();
    }
}