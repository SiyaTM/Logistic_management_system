namespace Core.Delivery.Commands;

using Common.Models.Delivery;
using System.Windows.Input;
using Common.Entities;
using Common.Data;
using LazyCache;
using Common.Results;

public sealed class CreateDeliveryCommand : ICommand<Result<DeliveryModel>>
{
    public CreateDeliveryModel? Data { get; set; }
}

public sealed class CreateDeliveryCommandHandler(
    DatabaseContext databaseContext, 
    IAppCache cache) : ICommandHandler<CreateDeliveryCommand, Result<DeliveryModel>>
{
    public async Task<Result<DeliveryModel>> Handle(CreateDeliveryCommand request, CancellationToken cancellationToken = default)
    {
        if (request.Data is null)
        {
            return Result<DeliveryModel>.Failure("Error");
        }

        var entity = new Delivery
        {
            Status = request.Data.Status,
            ScheduledDate = request.Data.ScheduledDate,
            DeliveredAt = request.Data.DeliveredAt
        };
        databaseContext.Deliveries.Add(entity);
        var result = await databaseContext.SaveChangesAsync(cancellationToken);

        // Invalidate cache after successful write
        cache.Remove(CacheData.CacheKey);

        return result > 0 ? Result<DeliveryModel>.Success(entity.Map()) : Result<DeliveryModel>.Failure("Error");
    }
}