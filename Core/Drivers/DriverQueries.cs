namespace Core.Driver;

public static class DriverQueries
{
	public static readonly Func<DatabaseContext, int, Task<Common.Entities.Driver?>> FindQuery
		= EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Drivers.FirstOrDefault(c => c.Id == id));
}
