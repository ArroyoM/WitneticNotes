using FluentValidation;
using Notes.Core.DTOs;

namespace Notes.Infrastructure.Validators
{
    public class NoteValidator : AbstractValidator<NoteDTO>
    {
        public NoteValidator()
        {
            RuleFor(note => note.IdBook)
                .NotEmpty()
                .NotNull();

            RuleFor(note => note.Name)
                .NotNull()
                .NotNull()
                .Length(1,699);
                
        }
    }
}
