using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]/facility")]
public abstract class BaseApiController : ControllerBase
{
    /// <summary>
    /// Get Mediator 
    /// </summary>
    public IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
    //protected IMediator Mediator
    //{
    //    get
    //    {
    //        return HttpContext.RequestServices.GetService<IMediator>();
    //    }
    //}
    //public BaseApiController(IMediator mediator)
    //{
    //    Mediator = mediator;
    //}

}
