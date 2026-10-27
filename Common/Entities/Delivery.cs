namespace Common.Entities;

using Common.Enums;

public class Delivery
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public DeliveryStatus Status { get; set; }
    public DateTime? ScheduledDate { get; set; } = DateTime.UtcNow;
    public DateTime? DeliveredAt { get; set; }
}
