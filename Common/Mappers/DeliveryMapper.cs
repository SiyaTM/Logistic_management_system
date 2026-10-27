using Common.Models.Delivery;
using System.Net.NetworkInformation;

namespace Common.Mappers;

public static class DeliveryMapper
{
    public static DeliveryModel Map(this Delivery entity)
        => new()
        { 
            Id = entity.Id,
            Status = entity.Status,
            ScheduledDate = entity.ScheduledDate,
            DeliveredAt = entity.DeliveredAt
        };

    public static Delivery Map(this DeliveryModel model)
    {
        var entity = new Delivery
        {
            Id = model.Id,
            Status = model.Status,
            ScheduledDate = model.ScheduledDate,
            DeliveredAt = model.DeliveredAt
        };

        return entity;
    }

    public static IEnumerable<DeliveryModel> Map(this List<Delivery> entites)
        => entites.Select(x => x.Map());

    public static IEnumerable<Delivery> Map(this List<DeliveryModel> models)
        => models.Select(x => x.Map());
}
