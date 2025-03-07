using FluentValidation;
using ToDoListApi.Dtos;

namespace ToDoListApi.Validators
{
    public class RegisterUseValidator : AbstractValidator<CreateUser>
    {
        public RegisterUseValidator()
        {
            RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MaximumLength(50).WithMessage("Username must not over 50 characters");

            RuleFor(x => x.PasswordHash)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters length");
        }
    }
}
