namespace Common.Mappers;

using Common.Models.Drivers;

public static class DriverMapper
{
	public static DriverModel Map(this Driver entity)
		=> new()
		{
			Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            LicenseNumber = entity.LicenseNumber,
			Phone = entity.Phone,
            Status = entity.Status,
		};

	public static Driver Map(this DriverModel model)
	{
		var entity = new Driver
		{
			Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
        };

		if (!string.IsNullOrEmpty(model.FirstName))
		{
			entity.FirstName = model.FirstName;
		}

		if (!string.IsNullOrEmpty(model.LastName))
		{
			entity.LastName = model.LastName;
		}

		return entity;
	}

	public static IEnumerable<DriverModel> Map(this List<Driver> entities)
		=> entities.Select(x => x.Map());

	public static IEnumerable<Driver> Map(this List<DriverModel> models)
		=> models.Select(x => x.Map());
}
