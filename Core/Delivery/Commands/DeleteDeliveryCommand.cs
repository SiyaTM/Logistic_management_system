namespace Core.Pizza.Commands;

using Common.Data;
using Common.Results;
using Core;
using LazyCache;

public sealed class DeleteDeliveryCommand : ICommand<Result>
{
	public int? Id { get; set; }
}


public sealed class DeleteDeliveryCommandHandler(DatabaseContext databaseContext, IAppCache cache) : ICommandHandler<DeleteDeliveryCommand, Result>
{
	public async Task<Result> Handle(DeleteDeliveryCommand request, CancellationToken cancellationToken)
	{
		if (request.Id is null)
		{
			return Result.Failure("Error");
		}

		var query = EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Deliveries.FirstOrDefault(c => c.Id == id));
		var findEntity = await query(databaseContext, request.Id.Value);
		if (findEntity is null)
		{
			return Result.Failure("Not found");
		}

		databaseContext.Deliveries.Remove(findEntity);
		var result = await databaseContext.SaveChangesAsync(cancellationToken);

        // Invalidate cache after successful write
        cache.Remove(CacheData.CacheKey);

        return result > 0 ? Result.Success() : Result.Failure("Error");
	}
}