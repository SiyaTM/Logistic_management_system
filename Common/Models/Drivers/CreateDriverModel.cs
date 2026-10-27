using Common.Enums;

namespace Common.Models.Drivers;

public sealed class CreateDriverModel
{
    public required string FirstName { get; set; }
    public string LastName { get; set; }

    public string? LicenseNumber { get; set; }

    public DriverStatus Status { get; set; }

    public string? Phone { get; set; }

}
