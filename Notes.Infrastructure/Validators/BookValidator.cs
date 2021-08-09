using FluentValidation;
using Notes.Core.DTOs;

namespace Notes.Infrastructure.Validators
{
    public class BookValidator : AbstractValidator<BookDTO>
    {
        public BookValidator()
        {

            RuleFor(book => book.IdUser)
                .NotEmpty()
                .NotNull();

            RuleFor(book => book.Name)
                .NotNull()
                .NotEmpty()
                .Length(1, 99);

            RuleFor(book => book.Description)
                .NotEmpty()
                .NotNull()
                .Length(1, 499);

            RuleFor(book => book.Color)
                .NotNull()
                .NotEmpty()
                .Length(2, 19);
        }
    }
}
