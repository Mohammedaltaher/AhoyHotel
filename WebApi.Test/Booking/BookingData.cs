using Application.Features.Booking.Commands;

namespace WebApi.Test.Booking;
public static class BookingData
{
    public static CreateBookingCommand MockCreateBookingCommand() => new()
    {
        CheckIn = DateTime.Now,
        CheckOut = DateTime.Now.AddDays(30),
        ActualPrice = 1000,
        Discount = 100,
        IsConfirmed = true,
        PaidAmount = 900,
        RoomId = 1,
        UserName = "Mohammed Eltaher"
    };
}
