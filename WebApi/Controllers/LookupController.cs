using Application.Features.FacilityFeatures.Queries;
using Application.Features.LookUp.Commands;
using Application.Features.LookUp.Queries;

namespace WebApi.Controllers;
public class LookupController : BaseApiController
{
    /// <summary>
    /// Creates a New Facility.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("facility")]
    public async Task<IActionResult> Create(CreateFacilityCommand command)
    {
        FacilityModel Facility = await Mediator.Send(command);
        return StatusCode(Facility.StatusCode,Facility.Data);
    }
    /// <summary>
    /// Gets all Facilitys.
    /// </summary>
    /// <returns></returns>
    [HttpPost("facility/getAll")]
    public async Task<IActionResult> GetAll(GetAllFacilityQuery query)
    {

        FacilitiesModel Facilities = await Mediator.Send(query);
        return StatusCode(Facilities.StatusCode, Facilities.Data);
    }
    /// <summary>
    /// Gets Facility by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("facility/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        FacilityModel Facility = await Mediator.Send(new GetFacilityByIdQuery { Id = id });
        return StatusCode(Facility.StatusCode,  Facility.Data);

    }
    /// <summary>
    /// Deletes Facility  based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("facility/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        FacilityModel Facility = await Mediator.Send(new DeleteFacilityByIdCommand { Id = id });
        return StatusCode(Facility.StatusCode,  Facility.Data);
    }
    /// <summary>
    /// Updates the Facility  based on Id.   
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("facility/{id}")]
    public async Task<IActionResult> Update(int id, UpdateFacilityCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        FacilityModel Facility = await Mediator.Send(command);
        return StatusCode(Facility.StatusCode,Facility.Data );
    }
}
