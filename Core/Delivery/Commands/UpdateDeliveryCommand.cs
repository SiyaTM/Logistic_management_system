namespace Core.Delivery.Commands;

using Common.Data;
using Common.Models.Delivery;
using Common.Results;
using LazyCache;
using Microsoft.EntityFrameworkCore;

public sealed class UpdateDeliveryCommand : ICommand<Result<DeliveryModel>>
{
	public int? Id { get; set; }
	public UpdateDeliveryModel? Data { get; set; }
}


public sealed class UpdateDeliveryCommandHandler(DatabaseContext databaseContext, IAppCache cache ) : ICommandHandler<UpdateDeliveryCommand, Result<DeliveryModel>>
{
	public async Task<Result<DeliveryModel>> Handle(UpdateDeliveryCommand request, CancellationToken cancellationToken)
	{
		if (request.Data is null || request.Id is null)
		{
			return Result<DeliveryModel>.Failure("Error");
		}

		var model = request.Data;
		var query = EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Deliveries.FirstOrDefault(c => c.Id == id));
		var findEntity = await query(databaseContext, request.Id.Value);
		if (findEntity is null)
		{
			return Result<DeliveryModel>.Failure("Not found");
		}

        findEntity.Description = !string.IsNullOrEmpty(model?.Description) ? model?.Description : findEntity.Description;

        var outcome = databaseContext.Deliveries.Update(findEntity);
		var result = await databaseContext.SaveChangesAsync(cancellationToken);

        // Invalidate cache after successful write
        cache.Remove(CacheData.CacheKey);

        return result > 0 ? Result<DeliveryModel>.Success(findEntity.Map()) : Result<DeliveryModel>.Failure("Error");
	}
}