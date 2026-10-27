namespace Common.Models.Delivery;

using Common.Enums;

public sealed class CreateDeliveryModel
{
    public DeliveryStatus Status { get; set; }
    public string? Description { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? DeliveredAt { get; set; }
}
