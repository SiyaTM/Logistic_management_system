namespace Core.Driver.Commands;

using Common.Data;
using Common.Models.Drivers;
using Common.Results;
using LazyCache;

public sealed class UpdateDriverCommand : ICommand<Result<DriverModel>>
{
	public int? Id { get; set; }
	public UpdateDriverModel? Data { get; set; }
}
public sealed class UpdateDriverCommandHandler(DatabaseContext databaseContext, IAppCache cache) : ICommandHandler<UpdateDriverCommand, Result<DriverModel>>
{
	public async Task<Result<DriverModel>> Handle(UpdateDriverCommand request, CancellationToken cancellationToken)
	{
		if (request.Data is null || request.Id is null)
		{
			return Result<DriverModel>.Failure("Not found");
		}

		var model = request.Data;
		var query = EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Drivers.FirstOrDefault(c => c.Id == id));
		var findEntity = await query(databaseContext, request.Id.Value);

		if (findEntity is null) {
			return Result<DriverModel>.Failure("Not found");
		}

        findEntity.FirstName = !string.IsNullOrEmpty(model?.FirstName) ? model?.FirstName : findEntity.FirstName;
        findEntity.LastName = !string.IsNullOrEmpty(model?.LastName) ? model?.LastName : findEntity.LastName;
		findEntity.LicenseNumber = !string.IsNullOrEmpty(model?.LicenseNumber) ? model?.LicenseNumber : findEntity.LicenseNumber;
		findEntity.Phone = !string.IsNullOrEmpty(model?.Phone) ? model?.Phone : findEntity.Phone;

        var oustomer = databaseContext.Drivers.Update(findEntity);
		var result = await databaseContext.SaveChangesAsync(cancellationToken);

        // Invalidate cache after successful write
        cache.Remove(CacheData.CacheKey);

        return result > 0 ? Result<DriverModel>.Success(findEntity.Map()) : Result<DriverModel>.Failure("Error");
	}
}