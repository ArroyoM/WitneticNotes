using FluentValidation;
using Notes.Core.DTOs;

namespace Notes.Infrastructure.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(user => user.Name)
                .NotNull()
                .NotEmpty()
                .Length(3, 100);

            RuleFor(user => user.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Length(5, 50);
        }
    }
}
