using FluentValidation;
using Notes.Core.Entities;

namespace Notes.Infrastructure.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLogin>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Length(5, 50);

            RuleFor(user => user.Password)
                .NotNull()
                .NotNull()
                .Length(5, 30);
        }
    }
}
