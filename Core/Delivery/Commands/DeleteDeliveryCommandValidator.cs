using Core.Pizza.Commands;

namespace Core.Delivery.Commands;

public sealed class DeleteDeliveryCommandValidator : AbstractValidator<DeleteDeliveryCommand>
{
    public DeleteDeliveryCommandValidator()
    {
        this.RuleFor(r => r.Id)
            .NotEmpty();
    }
}
