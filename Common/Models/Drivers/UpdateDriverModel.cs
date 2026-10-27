namespace Common.Models.Drivers;

public sealed class UpdateDriverModel
{
    public required string FirstName { get; set; }
    public string LastName { get; set; }

    public string? LicenseNumber { get; set; }

    public string? Phone { get; set; }
}
