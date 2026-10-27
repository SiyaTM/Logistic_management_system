namespace Common.Models.Delivery;

using Common.Enums;


public sealed class SearchDeliveryModel
{
    public DeliveryStatus Status { get; set; }
    public DateTime? ScheduledDate { get; set; }
    public DateTime? DeliveredAt { get; set; }
    public string? OrderBy { get; set; } = "ScheduledDate desc";
    //public PagingArgs PagingArgs { get; set; } = PagingArgs.NoPaging;

}
