using Application.Features.Booking.Commands;

namespace WebApi.Controllers;
public class BookingController : BaseApiController
{
    /// <summary>
    /// Creates a New Booking.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateBookingCommand command)
    {
        BookingModel booking = await Mediator.Send(command);
        return StatusCode(booking.StatusCode, booking.Data);
    }
}
