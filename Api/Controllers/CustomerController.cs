namespace Api.Controllers;

using Api.Helpers;
using Common.Models.Drivers;
using Core.Driver.Commands;
using Core.Driver.Queries;
using Microsoft.AspNetCore.Mvc;
using Core;
using Common.Results;

public class DriverController(Dispatcher dispatcher) : ApiController
{
	/// <summary>
	/// Get Driver by Id.
	/// </summary>
	/// <param name="id">int.</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	/// <response code="200">Get a Driver</response>
	/// <response code="400">Error getting a Driver</response>
	/// <response code="404">Driver not found</response>
	[HttpGet("{id}")]
	[ProducesResponseType(typeof(Result<DriverModel>), 200)]
	[ProducesResponseType(typeof(ErrorResult), 400)]
	[ProducesResponseType(typeof(ErrorResult), 404)]
	public async Task<ActionResult> GetDriver(int id)
	{
		var result = await dispatcher.Query(new GetDriverQuery { Id = id }, CancellationToken.None);
        return ResponseHelper.ResponseOutcome(result, this);
	}

	/// <summary>
	/// Get all Drivers.
	/// </summary>
	/// <returns>A <see cref="Task"/> repres
	/// enting the asynchronous operation.</returns>
	/// <response code="200">Driver Search</response>
	/// <response code="400">Error searching for Drivers</response>
	[HttpPost]
	[ProducesResponseType(typeof(Result<IEnumerable<DriverModel>>), 200)]
	[ProducesResponseType(typeof(ErrorResult), 400)]
	[Route("Search")]
	public async Task<ActionResult> Search(SearchDriverModel? data)
	{ 
		var result = await dispatcher.Query(
			new GetDriversQuery()
			{
				Data = data
			}, CancellationToken.None);
		return ResponseHelper.ResponseOutcome(result, this);
	}

	/// <summary>
	/// Create Driver.
	/// </summary>
	/// <remarks>
	/// Sample request:
	///     POST api/Driver
	///     {
	///       "name": "Person A",
	///       "address": "1 Tree Street, Pretoria, Gauteng",
	///       "email": "person.a@gmail.com"
	///       "cellphone": "0721230000"
	///     }.
	/// </remarks>
	/// <param name="model">DriverModel.</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	/// <response code="200">Driver created</response>
	/// <response code="400">Error creating a Driver</response>
	[HttpPost]
	[ProducesResponseType(typeof(Result<DriverModel>), 200)]
	[ProducesResponseType(typeof(ErrorResult), 400)]
	public async Task<ActionResult<DriverModel>> Create(CreateDriverModel model)
	{
		var result = await dispatcher.Send(new CreateDriverCommand
		{
			Data = model
		});

		return ResponseHelper.ResponseOutcome(result, this);
	}

	/// <summary>
	/// Update Driver.
	/// </summary>
	/// <remarks>
	/// Sample request:
	///     PUT api/Driver
	///     {
	///       "id": 1,
	///       "email": "person.a@gmail.com"
	///     }.
	/// </remarks>
	/// <param name="model">DriverModel.</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	/// <response code="200">Driver updated</response>
	/// <response code="400">Error updating a Driver</response>
	/// <response code="404">Driver not found</response>
	[HttpPut]
	[ProducesResponseType(typeof(Result<DriverModel>), 200)]
	[ProducesResponseType(typeof(ErrorResult), 400)]
	[ProducesResponseType(typeof(Result), 404)]
	public async Task<ActionResult> Update(UpdateDriverModel model)
	{
		var result = await dispatcher.Send(new UpdateDriverCommand
		{
			Data = model
		});

		return ResponseHelper.ResponseOutcome(result, this);
	}

	/// <summary>
	/// Remove Driver by Id.
	/// </summary>
	/// <param name="id">int.</param>
	/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
	/// <response code="200">Driver deleted</response>
	/// <response code="400">Error deleting a Driver</response>
	[HttpDelete("{id}")]
	[ProducesResponseType(typeof(Result), 200)]
	[ProducesResponseType(typeof(ErrorResult), 400)]
	public async Task<ActionResult> Delete(int id)
	{
		var result = await dispatcher.Send(new DeleteDriverCommand { Id = id });
		return ResponseHelper.ResponseOutcome(result, this);
	}
}
