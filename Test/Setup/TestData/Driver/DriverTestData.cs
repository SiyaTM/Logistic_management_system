namespace Test.Setup.TestData.Driver;

using Common.Models.Drivers;
using Common.Entities;
using Bogus;
using Common.Enums;

public static class DriverTestData
{
    public static Faker faker = new("en_ZA");

    public static Driver Driver = new()
    {
        Id = 1,
        FirstName = faker.Person.FirstName,
        LastName = faker.Person.LastName,
        Phone = faker.Phone.PhoneNumber(),
        LicenseNumber = faker.Random.String(),
        Status = DriverStatus.Available,
    };

    public static DriverModel DriverModel = new()
    {
        Id = 1,
        FirstName = faker.Person.FirstName,
        LastName = faker.Person.LastName,
        Phone = faker.Phone.PhoneNumber(),
        LicenseNumber = faker.Random.String(),
        Status = DriverStatus.Available,
    };
}
