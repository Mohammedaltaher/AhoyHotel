using Application.Features.lookup.Commands;
using Application.Features.lookup.Queries;

namespace WebApi.Controllers;
public class LookupController : BaseApiController
{
    /// <summary>
    /// Creates a New Facility.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateFacilityCommand command)
    {
        var facility = await Mediator.Send(command);
        return StatusCode(facility.StatusCode, facility.Data == null ? facility.Message : facility.Data);
    }
    /// <summary>
    /// Gets all Facility.
    /// </summary>
    /// <returns></returns>
    [HttpPost("getAll")]
    public async Task<IActionResult> GetAll(GetAllFacilityQuery query)
    {

        var facilities = await Mediator.Send(query);
        return StatusCode(facilities.StatusCode, facilities.Data == null ? facilities.Message : facilities.Data);
    }
    /// <summary>
    /// Gets Facility by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var facility = await Mediator.Send(new GetFacilityByIdQuery { Id = id });
        return StatusCode(facility.StatusCode, facility.Data == null ? facility.Message : facility.Data);

    }
    /// <summary>
    /// Deletes Facility  based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var facility = await Mediator.Send(new DeleteFacilityByIdCommand { Id = id });
        return StatusCode(facility.StatusCode, facility.Data == null ? facility.Message : facility.Data);
    }
    /// <summary>
    /// Updates the Facility  based on Id.   
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateFacilityCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        var facility = await Mediator.Send(command);
        return StatusCode(facility.StatusCode, facility.Data == null ? facility.Message : facility.Data);
    }
}
