namespace Core.Driver.Commands;

public sealed class CreateDriverCommandValidator : AbstractValidator<CreateDriverCommand>
{
    public CreateDriverCommandValidator() {
        this.RuleFor(r => r.Data.FirstName)
            .MaximumLength(100)
            .NotEmpty();

        this.RuleFor(r => r.Data.LastName)
            .MaximumLength(100)
            .NotEmpty();

        this.RuleFor(r => r.Data.LicenseNumber)
            .NotEmpty();

        this.RuleFor(r => r.Data.Phone)
            .NotEmpty();
    }
}
