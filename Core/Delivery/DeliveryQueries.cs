namespace Core.Delivery;

using Common.Entities;

public static class DeliveryQueries
{
    public static readonly Func<DatabaseContext, int, Task<Delivery?>> FindQuery
        = EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Deliveries.FirstOrDefault(x => x.Id == id));
}
