namespace Api.Controllers;

using Core;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public abstract class ApiController : ControllerBase
{
    private Dispatcher? dispatcher;
    protected Dispatcher Dispatcher => this.dispatcher ??= this.HttpContext.RequestServices.GetRequiredService<Dispatcher>();
}
