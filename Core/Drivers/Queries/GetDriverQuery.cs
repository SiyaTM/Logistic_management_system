namespace Core.Driver.Queries;

using Common.Models.Drivers;
using Common.Results;

public sealed class GetDriverQuery : IQuery<Result<DriverModel>>
{
	public int Id { get; set; }
};

public sealed class GetDriverQueryHandler(DatabaseContext databaseContext) : IQueryHandler<GetDriverQuery, Result<DriverModel>>
{
	public async Task<Result<DriverModel>> Handle(GetDriverQuery request, CancellationToken cancellationToken)
	{
		var query = EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Drivers.FirstOrDefault(c => c.Id == id));
		var entity = await query(databaseContext, request.Id);
		return entity is null
			? Result<DriverModel>.Failure("Not Found")
			: Result<DriverModel>.Success(entity.Map());
	}
}