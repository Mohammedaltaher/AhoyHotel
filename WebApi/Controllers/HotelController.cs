using Application.Features.Common.Commands;
using Application.Features.Hotel.Commands;
using Application.Features.Hotel.Queries;

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
        var hotel = await Mediator.Send(command);
        return StatusCode(hotel.StatusCode, hotel.Data);
    }
    /// <summary>
    /// Add Hotel Facility.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("add/facility")]
    public async Task<IActionResult> AddHotelFacility(CreateHotelFacilityCommand command)
    {
        var hotelFacility = await Mediator.Send(command);
        return StatusCode(hotelFacility.StatusCode, hotelFacility.Message);

    }
    /// <summary>
    /// Add Hotel Room.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("add/room")]
    public async Task<IActionResult> AddHotelRoom(CreateHotelRoomsCommand command)
    {
        var hotelRoom = await Mediator.Send(command);
        return StatusCode(hotelRoom.StatusCode, hotelRoom.Message);

    }
    /// <summary>
    /// Add Hotel Review.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost("add/review")]
    public async Task<IActionResult> AddHotelReview(CreateHotelReviewCommand command)
    {
        var hotelReview = await Mediator.Send(command);
        return StatusCode(hotelReview.StatusCode, hotelReview.Message);

    }
    /// <summary>
    /// Add Hotel Image.
    /// </summary>
    /// <param name="image"></param>
    /// <param name="hotelId"></param>
    /// <returns></returns>
    [HttpPost("add/image")]
    public async Task<IActionResult> AddHotelImage(IFormFile image, int hotelId)
    {
        if (!IsWrongFileExtension(image))
        {
            return BadRequest("wrong file extension");
        }
        var imageUrl = await Mediator.Send(new UploadFileCommand
        {
            FormFile = image,
            Path = Directory.GetCurrentDirectory() + @"\Uploads\HotelImages",
        });
        var hotelImage = await Mediator.Send(new CreateHotelImageCommand { HotelId = hotelId, Url = imageUrl });
        return StatusCode(hotelImage.StatusCode, hotelImage.Message);
    }
    /// <summary>
    /// Search Hotels.
    /// </summary>
    /// <returns></returns>
    [HttpPost("search")]
    public async Task<IActionResult> GetAll(SearchHotelsQuery query)
    {
        var hotels = await Mediator.Send(query);
        return StatusCode(hotels.StatusCode, hotels.Data == null ? hotels.Message : hotels.Data);
    }
    /// <summary>
    /// Gets Hotel  by Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var hotel = await Mediator.Send(new GetHotelByIdQuery { Id = id });
        return StatusCode(hotel.StatusCode, hotel.Data == null ? hotel.Message : hotel.Data);

    }
    /// <summary>
    /// Deletes Hotel  based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var hotel = await Mediator.Send(new DeleteHotelByIdCommand { Id = id });
        return StatusCode(hotel.StatusCode, hotel.Data == null ? hotel.Message : hotel.Data);
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
        var hotel = await Mediator.Send(command);
        return StatusCode(hotel.StatusCode, hotel.Data == null ? hotel.Message : hotel.Data);
    }
    private bool IsWrongFileExtension(IFormFile file)
    {
        var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1].ToLower();
        return (extension == ".pdf" || extension == ".doc" || extension == ".docx" || extension == ".png" || extension == ".jpg" || extension == ".jpeg" || extension == ".gif");
    }

}
