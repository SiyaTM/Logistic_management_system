namespace Core.Driver.Commands;

public sealed class DeleteDriverCommandValidator : AbstractValidator<DeleteDriverCommand>
{
    public DeleteDriverCommandValidator()
    {
        this.RuleFor(r => r.Id)
            .NotEmpty();
    }
}
