namespace Core.Delivery.Commands;

public sealed class UpdateDeliveryCommandValidator : AbstractValidator<UpdateDeliveryCommand>
{
    public UpdateDeliveryCommandValidator()
    {
        this.RuleFor(r => r.Data)
            .NotNull();

        this.RuleFor(r => r.Data.Description)
            .MaximumLength(500);
    }
}
