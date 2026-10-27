namespace Test.Setup.TestData.Delivery;

using Common.Models.Delivery;
using Common.Enums;

public static class DeliveryTestData
{

    public static DeliveryModel Delivery => new()
    {
        Id = 1,
        Status = DeliveryStatus.Scheduled,
        DeliveredAt = DateTime.Now,
        ScheduledDate = DateTime.Now,
    };

    public static DeliveryModel DeliveryModel => new()
    {
        Id = 1,
        Status = DeliveryStatus.Scheduled,
        DeliveredAt = DateTime.Now,
        ScheduledDate = DateTime.Now,
    };
}
