namespace Core.Driver.Commands;

public sealed class UpdateDriverCommandValidator : AbstractValidator<UpdateDriverCommand>
{
    public UpdateDriverCommandValidator() {
        this.RuleFor(r => r.Data)
            .NotEmpty();

        this.RuleFor(r => r.Id)
            .NotEmpty();

        this.RuleFor(r => r.Data.FirstName)
            .MaximumLength(100);

        this.RuleFor(r => r.Data.LastName)
            .MaximumLength(100);


        this.RuleFor(r => r.Data.LicenseNumber)
            .NotEmpty();

        this.RuleFor(r => r.Data.Phone)
            .NotEmpty();

    }
}
