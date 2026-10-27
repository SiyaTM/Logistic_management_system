namespace Api.Controllers;

using Api.Helpers;
using Common.Models.Delivery;
using Core;
using Core.Delivery.Commands;
using Core.Delivery.Queries;
using Microsoft.AspNetCore.Mvc;
using Common.Entities;

[ApiController]
[Route("{controller}")]
public class DeliveryController(Dispatcher dispatcher) : ApiController
{
    /// <summary>
    /// Get Delivery by Id.
    /// </summary>
    /// <param name="id">Delivery Id</param>
    /// <returns>ActionResult</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(404)]
    public async Task<ActionResult> Get(int id)
    {
        var result = await dispatcher.Query(new GetDeliveryQuery { Id = id }, CancellationToken.None);
        return ResponseHelper.ResponseOutcome(result, this);
    }

    /// <summary>
    /// Get all Deliveries.
    /// </summary>
    /// <returns>ActionResult</returns>
    [HttpPost("Search")]
    [ProducesResponseType(200)]
    public async Task<ActionResult> Search(SearchDeliveryModel search)
    {
        var result = await dispatcher.Query(new GetDeliveriesQuery()
        {
            Search = search
        }, CancellationToken.None);
        return ResponseHelper.ResponseOutcome(result, this);
    }

    /// <summary>
    /// Create Delivery.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     POST api/Delivery
    ///     {
    ///       "Status": "Scheduled",
    ///       "Schedulted at": "2026/10/25"
    ///       "Delivered at": "2026/10/30
    ///     }
    /// </remarks>
    /// <param name="model">Delivery Model</param>
    /// <returns>ActionResult</returns>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult<Delivery>> Create([FromBody] CreateDeliveryModel model)
    {
        var result = await dispatcher.Send(new CreateDeliveryCommand
        {
            Data = model
        });

        return ResponseHelper.ResponseOutcome(result, this);
    }

    /// <summary>
    /// Update Delivery.
    /// </summary>
    /// <remarks>
    /// Sample request:
    ///
    ///     PUT api/Delivery/1
    ///     {
    ///       "status": "InTransit"
    ///     }
    /// </remarks>
    /// <param name="id"></param>
    /// <param name="model">Delivery Model</param>
    /// <returns>ActionResult</returns>
    [HttpPut]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Update([FromBody] UpdateDeliveryModel model)
    {
        var result = await dispatcher.Send(new UpdateDeliveryCommand
        {
            Data = model
        });

        return ResponseHelper.ResponseOutcome(result, this);
    }

    /// <summary>
    /// Delete Delivery by Id.
    /// </summary>
    /// <param name="id">Delivery Id</param>
    /// <returns>ActionResult</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await dispatcher.Send(new DeleteDeliveryCommand { Id = id });
        return ResponseHelper.ResponseOutcome(result, this);
    }

}
