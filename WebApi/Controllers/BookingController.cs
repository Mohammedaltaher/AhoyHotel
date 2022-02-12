using Application.Features.Booking.Commands;
using Application.Features.Common.Commands;
using Application.Features.HotelFeatures.Commands;
using Application.Features.HotelFeatures.Queries;
using Application.Model;
using System.IO;
using System.Threading;

namespace WebApi.Controllers;
public class BookingController : BaseApiController
{
    /// <summary>
    /// Creates a New Hotel.
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
