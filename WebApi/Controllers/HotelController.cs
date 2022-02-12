using Application.Features.Common.Commands;
using Application.Features.HotelFeatures.Commands;
using Application.Features.HotelFeatures.Queries;

namespace WebApi.Controllers;
public class HotelController : BaseApiController
{
    /// <summary>
    /// Creates a New Hotel.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateHotelCommand command)
    {
        HotelModel Hotel = await Mediator.Send(command);
        return StatusCode(Hotel.StatusCode, Hotel.Data);
    }
    /// <summary>
    /// Add Hotel Facility.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("add/facility")]
    public async Task<IActionResult> AddHotelFacility(CreateHotelFacilityCommand command)
    {
        BaseModel HotelFacility = await Mediator.Send(command);
        return StatusCode(HotelFacility.StatusCode, HotelFacility.Messege);

    }

    /// <summary>
    /// Add Hotel Room.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("add/room")]
    public async Task<IActionResult> AddHotelRoom(CreateHotelRoomsCommand command)
    {
        BaseModel HotelRoom = await Mediator.Send(command);
        return StatusCode(HotelRoom.StatusCode, HotelRoom.Messege);

    }
    /// <summary>
    /// Add Hotel Review.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("add/review")]
    public async Task<IActionResult> AddHotelReview(CreateHotelReviewCommand command)
    {
        BaseModel HotelReview = await Mediator.Send(command);
        return StatusCode(HotelReview.StatusCode, HotelReview.Messege);

    }
    /// <summary>
    /// Add Hotel Image.
    /// </summary>
    /// <param name="image"></param>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [HttpPost("add/image")]
    /// <param name="image"></param>
    public async Task<IActionResult> AddHotelImage(IFormFile image, int hotelId)
    {
        if (!IsWrongFileExtension(image))
        {
            return BadRequest("wrong file extension");
        }
        string ImageUrl = await Mediator.Send(new UploadFileCommand
        {
            FormFile = image,
            Path = Directory.GetCurrentDirectory() + @"\Uploads\HotelImages",
        });
        BaseModel Hotelimage = await Mediator.Send(new CreateHotelImageCommand { HotelId = hotelId, Url = ImageUrl });
        return StatusCode(Hotelimage.StatusCode, Hotelimage.Messege);
    }
    /// <summary>
    /// Search Hotels.
    /// </summary>
    /// <returns></returns>

    [HttpPost("search")]
    public async Task<IActionResult> GetAll(SearchHotelsQuery query)
    {
        HotelsModel Hotels = await Mediator.Send(query);
        return StatusCode(Hotels.StatusCode, Hotels.Data);
    }
    /// <summary>
    /// Gets Hotel  by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        HotelModel Hotel = await Mediator.Send(new GetHotelByIdQuery { Id = id });
        return StatusCode(Hotel.StatusCode, Hotel.Data);

    }
    /// <summary>
    /// Deletes Hotel  based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        HotelModel Hotel = await Mediator.Send(new DeleteHotelByIdCommand { Id = id });
        return StatusCode(Hotel.StatusCode, Hotel.Data);
    }
    /// <summary>
    /// Updates the  Entity based on Id.   
    /// </summary>
    /// <param name="id"></param>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateHotelCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        HotelModel Hotel = await Mediator.Send(command);
        return StatusCode(Hotel.StatusCode, Hotel.Data);
    }

    private bool IsWrongFileExtension(IFormFile file)
    {
        string extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1].ToLower();
        return (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif");
    }

}
