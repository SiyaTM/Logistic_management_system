namespace Core.Driver.Queries;

using Common.Models;
using Common.Models.Delivery;
using Common.Models.Drivers;
using Common.Results;
using Core.Delivery.Queries;
using DataAccess.Filters;
using LazyCache;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Common.Data;

public sealed class GetDriversQuery : IQuery<PagedResults<IEnumerable<DriverModel>>>
{
    public SearchDriverModel Search { get; set; } = new();
    public PagingArgs Paging { get; set; } = new() { PageNumber = 1, PageSize = 10 };

}

public sealed class GetDriversQueryHandler(DatabaseContext databaseContext, IAppCache cache) : IQueryHandler<GetDriversQuery, PagedResults<IEnumerable<DriverModel>>>
{
    private readonly TimeSpan cacheExpiry = new(12, 0, 0);
    public async Task<PagedResults<IEnumerable<DriverModel>>> Handle(GetDriversQuery request, CancellationToken cancellationToken)
    {
        // Safe null handling for request and inner objects
        request ??= new GetDriversQuery();
        var search = request.Search ?? new SearchDriverModel();
        var paging = request.Paging ?? new PagingArgs { PageNumber = 1, PageSize = 10 };

        // cached data
        Task<IEnumerable<DriverModel>> DataDelegate() => this.GetData();
        var cachedData = await cache.GetOrAddAsync(CacheData.CacheKey, DataDelegate, this.cacheExpiry);

        if (cachedData != null)
        {
            var data = cachedData?
                .FilterByFirstName(search.FirstName)
                .FilterByLastName(search.LastName)
                .FilterByLicenseNumber(search.LicenseNumber)
                .FilterByPhone(search.Phone)
                .AsQueryable();

            return PagedResults<IEnumerable<DriverModel>>.Success(data, cachedData.Count());
        }

        // Cache miss: query database
        var query = databaseContext.Drivers
            .Select(x => x)
            .AsNoTracking()
			.FilterByFirstName(search.FirstName)
			.FilterByLastName(search.LastName)
			.FilterByLicenseNumber(search.LicenseNumber)
			.FilterByPhone(search.Phone)
            .OrderBy(search.OrderBy);


         var totalCount = await query.CountAsync(cancellationToken);

        // Safe paging with defualts
        int pageNumber = paging.PageNumber > 0 ? paging.PageNumber : 1;
        int pageSize = paging.PageSize > 0 ? paging.PageSize : 10;

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(d => new DriverModel
            {
                Id = d.Id,
                FirstName = d.FirstName,
                LastName = d.LastName,
                LicenseNumber = d.LicenseNumber,
                Status = d.Status,
                Phone = d.Phone
            })
            .ToListAsync(cancellationToken);

        return PagedResults<IEnumerable<DriverModel>>.Success(items, totalCount, pageNumber, pageSize);
    }

    private async Task<IEnumerable<DriverModel>> GetData()
    {
        var entities = await databaseContext.Drivers.Select(x => x)
            .AsNoTracking()
            .ToListAsync();

        return entities.Map();
    }
}