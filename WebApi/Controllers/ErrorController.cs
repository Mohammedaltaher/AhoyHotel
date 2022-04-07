using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers;
public class ErrorController : BaseApiController
{
    private readonly ILogger _logger;
    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }
    /// <summary>
    /// Development error
    /// </summary>
    /// <param name="webHostEnvironment"></param>
    /// <returns></returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [Route("/development")]
    public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
    {
        var error = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (error != null && error.GetType().IsAssignableFrom(typeof(ValidationException)))
        {
            return BadRequest(error.Message);
        }

        if (error != null)
        {
            var problem = Problem(detail: error.StackTrace, title: error.Message);

            _logger.LogError(problem.ToString());

            return problem;
        }

        return BadRequest();
    }
    /// <summary>
    /// Live Error
    /// </summary>
    /// <returns></returns>
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    [Route("/live")]
    public IActionResult Error()
    {
        var error = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        if (error != null)
        {
            _logger.LogError(Problem(detail: error.StackTrace, title: error.Message).ToString());

            return Problem(title: error.Message);
        }

        return BadRequest();
    }
}
