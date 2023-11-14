using FluentValidation;
using StudentsDbApp.DTO;

namespace StudentsDbApp.Validators
{
    public class StudentInsertValidator : AbstractValidator<StudentInsertDTO>

    {
        public StudentInsertValidator()
        {
            RuleFor(s => s.FirstName).NotEmpty()
                .WithMessage("Το πεδίο Ονομα δεν μπορεί να είναι κενό")
                .Length(2, 255).WithMessage("Το πεδίο Ονομα πρεπει να είναι μεταξυ 2-255");
            RuleFor(s => s.LastName).NotEmpty()
                .WithMessage("Το πεδίο Επωνυμο δεν μπορεί να είναι κενό")
                .Length(2, 255).WithMessage("Το πεδίο Επωνυμο πρεπει να είναι μεταξυ 2-255");
        }
    }
}
