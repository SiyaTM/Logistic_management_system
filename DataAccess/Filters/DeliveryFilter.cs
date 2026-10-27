namespace DataAccess.Filters;

using Common.Entities;
using System.Xml.Linq;
using Common.Models.Delivery;

public static class DeliveryFilter
{
    /*
    public static IQueryable<Delivery> FilterByStatus(this IQueryable<Delivery> query, Enum? status)
        => Enum.GetName(status) ? query : query.Where(x => x.Status.Contains(status));
    */
    public static IEnumerable<DeliveryModel> FilterByDeliveredAt(this IEnumerable<DeliveryModel> query, DateTime? deliveredAt)
        => !deliveredAt.HasValue ? query : query.Where(x => x.DeliveredAt == deliveredAt.Value);

    public static IQueryable<Delivery> FilterByDeliveredAt(this IQueryable<Delivery> query, DateTime? deliveredAt)
        => !deliveredAt.HasValue ? query : query.Where(x => x.DeliveredAt == deliveredAt.Value);

    public static IEnumerable<DeliveryModel> FilterByScheduledDate(this IEnumerable<DeliveryModel> query, DateTime? scheduledDate)
        => !scheduledDate.HasValue ? query : query.Where(x => x.ScheduledDate == scheduledDate.Value);

    public static IQueryable<Delivery> FilterByScheduledDate(this IQueryable<Delivery> query, DateTime? scheduledDate)
        => !scheduledDate.HasValue ? query : query.Where(x => x.ScheduledDate == scheduledDate.Value);
}
