namespace Core.Driver.Commands;

using Common.Data;
using Common.Entities;
using Common.Models.Drivers;
using Common.Results;
using Core.Driver.Commands;
using LazyCache;

public sealed class CreateDriverCommand : ICommand<Result<DriverModel>>
{ 
    public CreateDriverModel? Data { get; set; }
}

public sealed class CreateDriverCommandHandler(
    DatabaseContext databaseContext,
    IAppCache cache) : ICommandHandler<CreateDriverCommand, Result<DriverModel>>
{
    public async Task<Result<DriverModel>> Handle(CreateDriverCommand request, CancellationToken cancellationToken)
    {
        if (request.Data == null)
        {
            return Result<DriverModel>.Failure($"Error");
        }

        var entity = new Driver
        {
            FirstName = request.Data.FirstName,
            LastName = request.Data.LastName,
            LicenseNumber = request.Data.LicenseNumber,
            Status = request.Data.Status,
            Phone = request.Data.Phone
        };

        databaseContext.Drivers.Add(entity);
        var result = await databaseContext.SaveChangesAsync(cancellationToken);

        // Invalidate cache after successful write
        cache.Remove(CacheData.CacheKey);

        return result > 0 ? Result<DriverModel>.Success(entity.Map()) : Result<DriverModel>.Failure($"Error");
    }
}