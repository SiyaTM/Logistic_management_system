namespace Test.Core;

using Common.Models.Delivery;
using global::Core.Delivery.Commands;
using global::Core.Delivery.Queries;
using global::Core.Pizza.Commands;
using Test.Setup;
using Test.Setup.TestData.Delivery;

[TestFixture]
public class TestDeliveryCore : QueryTestBase
{
    private DeliveryModel model;

    [SetUp]
    public async Task Init()
    {
        this.model = DeliveryTestData.DeliveryModel;
        var sutCreate = new CreateDeliveryCommandHandler(this.Context, this.CachingService);
        var resultCreate = await sutCreate.Handle(
            new CreateDeliveryCommand
            {
                Data = new CreateDeliveryModel
                {
                    Status = this.model.Status,
                    DeliveredAt = DateTime.UtcNow,
                    ScheduledDate = DateTime.UtcNow,
                }
            }, CancellationToken.None);

        if(!resultCreate.Succeeded)
        {
            Assert.That(false, Is.True);
        }

        this.model = resultCreate.Data;
    }

    [Test]
    public async Task GetAsync()
    {
        var sutGet = new GetDeliveryQueryHandler(this.Context);
        var resultGet = await sutGet.Handle(
            new GetDeliveryQuery
            {
                Id = this.model.Id,
            }, CancellationToken.None);

        Assert.That(resultGet?.Data, Is.Not.Null);
    }

    [Test]
    public async Task GetAllAsync()
    {
        var query = new GetDeliveriesQuery
        {
            Search = new SearchDeliveryModel(),
            Paging = new PagingArgs { PageNumber = 1, PageSize = 10 }
        };
        var sutGetAll = new GetDeliveriesQueryHandler(this.Context, this.CachingService);
        var resultGetAll = await sutGetAll.Handle(query, CancellationToken.None);

        Assert.That(resultGetAll.TotalCount, Is.GreaterThanOrEqualTo(1));
    }

    [Test]
    public void SaveAsync() => Assert.That(this.model, Is.Not.Null);

    [Test]
    public async Task UpdateAsync()
    {
        var sutUpdate = new UpdateDeliveryCommandHandler(this.Context, this.CachingService);
        var resultUpdate = await sutUpdate.Handle(
            new UpdateDeliveryCommand
            {
                Id = this.model.Id,
                Data = new UpdateDeliveryModel
                {
                    Description = "A fragile box weighing 30 kilograms"
                }
            }, CancellationToken.None);

        Assert.That(resultUpdate.Succeeded, Is.True);
    }

    [Test]
    public async Task DeleteAsync()
    {
        var sutDelete = new DeleteDeliveryCommandHandler(this.Context, this.CachingService);
        var outcomeDelete = await sutDelete.Handle(
            new DeleteDeliveryCommand
            {
                Id = this.model.Id
            }, CancellationToken.None);

        Assert.That(outcomeDelete.Succeeded, Is.True);
    }
}
