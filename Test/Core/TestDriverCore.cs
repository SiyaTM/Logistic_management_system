namespace Test.Core;

using Common.Enums;
using Common.Models.Drivers;
using global::Core.Driver.Commands;
using global::Core.Driver.Queries;
using Test.Setup;
using Test.Setup.TestData.Driver;


[TestFixture]
public class TestDriverCore : QueryTestBase
{
	private DriverModel model;

	[SetUp]
	public async Task Init()
	{
		this.model = DriverTestData.DriverModel;
		var sutCreate = new CreateDriverCommandHandler(this.Context, this.CachingService);
		var resultCreate = await sutCreate.Handle(
			new CreateDriverCommand
			{
				Data = new CreateDriverModel
				{
                    FirstName = "John",
                    LastName = "Doe",
                    Phone = "+1234567890",
                    LicenseNumber = "DL-998822",
                    Status = DriverStatus.Available,
                }
			}, CancellationToken.None);

		if (!resultCreate.Succeeded)
		{
			Assert.That(false, Is.True);
		}

		this.model = resultCreate.Data;
	}

	[Test]
	public async Task GetAsync()
	{
		var sutGet = new GetDriverQueryHandler(this.Context);
		var resultGet = await sutGet.Handle(
			new GetDriverQuery
            {
				Id = this.model.Id
			}, CancellationToken.None);

		Assert.That(resultGet?.Data, Is.Not.Null);
	}

	[Test]
	public async Task GetAllAsync()
	{
        var query = new GetDriversQuery
        {
            Search = new SearchDriverModel
            {
                FirstName = "John"
            },
            Paging = new PagingArgs { PageNumber = 1, PageSize = 10 }
        };

        var sutGetAll = new GetDriversQueryHandler(this.Context, this.CachingService);
        var resultGetAll = await sutGetAll.Handle(new GetDriversQuery(), CancellationToken.None);

        Assert.That(resultGetAll?.Search, Is.Not.Null);
        Assert.That(resultGetAll?.TotalCount, Is.EqualTo(1));
    }

	[Test]
	public void SaveAsync() => Assert.That(this.model, Is.Not.Null);

	[Test]
	public async Task UpdateAsync()
	{
		var sutUpdate = new UpdateDriverCommandHandler(this.Context, this.CachingService);
		var resultUpdate = await sutUpdate.Handle(
			new UpdateDriverCommand
			{
				Id = this.model.Id,
				Data = new UpdateDriverModel
				{
                    FirstName = "Bill",
                    LastName = "Burr",
                    LicenseNumber = "123456",
                    Phone = "0721230000"
                }
			}, CancellationToken.None);

		Assert.That(resultUpdate.Succeeded, Is.True);
	}

	[Test]
	public async Task DeleteAsync()
	{
		var sutDelete = new DeleteDriverCommandHandler(this.Context, this.CachingService);
		var outcomeDelete = await sutDelete.Handle(
			new DeleteDriverCommand
			{
				Id = this.model.Id
			}, CancellationToken.None);

		Assert.That(outcomeDelete.Succeeded, Is.True);
	}
}
