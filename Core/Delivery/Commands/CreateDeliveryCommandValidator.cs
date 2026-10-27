namespace Core.Delivery.Commands;

public sealed class CreateDeliveryCommandValidator : AbstractValidator<CreateDeliveryCommand>
{
	public CreateDeliveryCommandValidator()
	{
		this.RuleFor(r => r.Data.Status)
			.NotEmpty();
	}

}
