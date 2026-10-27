namespace DataAccess.Filters;

using Common.Entities;
using Common.Models.Drivers;

public static class DriverFilter
{
    public static IEnumerable<DriverModel> FilterByFirstName(this IEnumerable<DriverModel> query, string firstname)
        => string.IsNullOrWhiteSpace(firstname) ? query : query.Where(x => x.FirstName.Contains(firstname));
    public static IQueryable<Driver> FilterByFirstName(this IQueryable<Driver> query, string firstname) 
        => string.IsNullOrWhiteSpace(firstname) ? query : query.Where(x => x.FirstName.Contains(firstname));
    public static IEnumerable<DriverModel> FilterByLastName(this IEnumerable<DriverModel> query, string lastname)
     => string.IsNullOrWhiteSpace(lastname) ? query : query.Where(x => x.FirstName.Contains(lastname));
    public static IQueryable<Driver> FilterByLastName(this IQueryable<Driver> query, string lastname) 
        => string.IsNullOrWhiteSpace(lastname) ? query : query.Where(x => x.LastName.Contains(lastname));
    public static IEnumerable<DriverModel> FilterByLicenseNumber(this IEnumerable<DriverModel> query, string licensenumber)
        => string.IsNullOrWhiteSpace(licensenumber) ? query : query.Where(x => x.LicenseNumber.Contains(licensenumber));
    public static IQueryable<Driver> FilterByLicenseNumber(this IQueryable<Driver> query, string licensenumber) 
        => string.IsNullOrWhiteSpace(licensenumber) ? query : query.Where(x => x.LicenseNumber.Contains(licensenumber));

    public static IEnumerable<DriverModel> FilterByPhone(this IEnumerable<DriverModel> query, string phone)
    => string.IsNullOrWhiteSpace(phone) ? query : query.Where(x => x.LicenseNumber.Contains(phone));
    public static IQueryable<Driver> FilterByPhone(this IQueryable<Driver> query, string phone) 
        => string.IsNullOrWhiteSpace(phone) ? query : query.Where(x => x.Phone.Contains(phone));

}
