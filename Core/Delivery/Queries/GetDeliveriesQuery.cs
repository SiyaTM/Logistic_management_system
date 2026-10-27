namespace Core.Delivery.Queries;

using Common.Models;
using Common.Models.Delivery;
using Common.Results;
using DataAccess;
using DataAccess.Filters;
using LazyCache;

public sealed class GetDeliveriesQuery : IQuery<PagedResults<IEnumerable<DeliveryModel>>>
{
    public SearchDeliveryModel Search { get; set; } = new();
    public PagingArgs Paging { get; set; } = new() { PageNumber = 1, PageSize = 10 };
};

public sealed class GetDeliveriesQueryHandler(DatabaseContext databaseContext, IAppCache cache) : IQueryHandler<GetDeliveriesQuery, PagedResults<IEnumerable<DeliveryModel>>>
{
    private readonly TimeSpan cacheExpiry = new(12, 0, 0);

    public async Task<PagedResults<IEnumerable<DeliveryModel>>> Handle(GetDeliveriesQuery request, CancellationToken cancellationToken)
    {
        // Safe null handling for request and inner objects
        request ??= new GetDeliveriesQuery();
        var search = request.Search ?? new SearchDeliveryModel();
        var paging = request.Paging ?? new PagingArgs { PageNumber = 1, PageSize = 10 };

        // cached data
        Task<IEnumerable<DeliveryModel>> DataDelegate() => this.GetData();
        var cachedData = await cache.GetOrAddAsync(Common.Data.CacheData.CacheKey, DataDelegate, this.cacheExpiry);

        if (cachedData != null)
        {
            var data = cachedData?
                .FilterByDeliveredAt(search.DeliveredAt)
                .FilterByScheduledDate(search.ScheduledDate)
                .AsQueryable();

            return PagedResults<IEnumerable<DeliveryModel>>.Success(data, cachedData.Count());
        }

        // Cache miss: query database
        var query = databaseContext.Deliveries
            .Select(x => x)
            .FilterByDeliveredAt(search.DeliveredAt)
            .FilterByScheduledDate(search.ScheduledDate)
            .OrderBy(search.OrderBy);

        var totalCount = await query.CountAsync(cancellationToken);

        // Safe paging with defualts
        int pageNumber = paging.PageNumber > 0 ? paging.PageNumber : 1;
        int pageSize = paging.PageSize > 0 ? paging.PageSize : 10;

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => x)
            .ToListAsync(cancellationToken);

        return PagedResults<IEnumerable<DeliveryModel>>.Success(items.Map(), totalCount, pageNumber, pageSize);
    }

    private async Task<IEnumerable<DeliveryModel>> GetData()
    {
        var entities = await databaseContext.Deliveries.Select(x => x)
            .AsNoTracking()
            .ToListAsync();

        return entities.Map();
    }
}