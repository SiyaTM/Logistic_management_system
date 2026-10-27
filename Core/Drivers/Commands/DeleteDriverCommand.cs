namespace Core.Driver.Commands;

using Common.Results;
using LazyCache;

public sealed class DeleteDriverCommand : ICommand<Result>
{
	public int? Id { get; set; }
}

public sealed class DeleteDriverCommandHandler(DatabaseContext databaseContext, IAppCache cache) : ICommandHandler<DeleteDriverCommand, Result>
{
	public async Task<Result> Handle(DeleteDriverCommand request, CancellationToken cancellationToken)
	{
		if (request.Id is null)
		{
			return Result.Failure("Error");
		}

        var query = EF.CompileAsyncQuery((DatabaseContext db, int id) =>
			db.Drivers.FirstOrDefault(c => c.Id == id));
        var findEntity = await query(databaseContext, request.Id.Value);
        if (findEntity is null)
        {
            return Result.Failure("Not found");
        }

        databaseContext.Drivers.Remove(findEntity);
        var result = await databaseContext.SaveChangesAsync(cancellationToken);

        // Invalidate cache after successful write
        cache.Remove(Common.Data.CacheData.CacheKey);

        return result > 0 ? Result.Success() : Result.Failure("Error");
	}
}