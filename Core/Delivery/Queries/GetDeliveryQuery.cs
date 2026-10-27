namespace Core.Delivery.Queries;

using Common.Models.Delivery;
using Common.Results;

public sealed class GetDeliveryQuery : IQuery<Result<DeliveryModel>>
{
    public int Id { get; set; }
};

public sealed class GetDeliveryQueryHandler(DatabaseContext databaseContext) : IQueryHandler<GetDeliveryQuery, Result<DeliveryModel>>
{
    public async Task<Result<DeliveryModel>> Handle(GetDeliveryQuery request, CancellationToken cancellationToken = default)
    {
        var query = EF.CompileAsyncQuery((DatabaseContext db, int id) => db.Deliveries.FirstOrDefault(c => c.Id == id));
        var entity = await query(databaseContext, request.Id);
        return entity is null
            ? Result<DeliveryModel>.Failure("Not Found")
            : Result<DeliveryModel>.Success(entity.Map());
    }
}
