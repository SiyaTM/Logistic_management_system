using Common.Enums;

namespace Common.Models.Drivers;

public sealed class SearchDriverModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string? LicenseNumber { get; set; }

    public DriverStatus Status { get; set; }

    public string? Phone { get; set; }
    public string? OrderBy { get; set; }
    //public PagingArgs PagingArgs { get; set; } = PagingArgs.NoPaging;

}
