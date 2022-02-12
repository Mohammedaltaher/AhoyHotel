using Application.Features.Booking.Commands;

namespace Application.Common.Mapper;
public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, CreateBookingCommand>();
        CreateMap<CreateBookingCommand, Booking>();
        CreateMap<Booking, BookingDto>();
    }
}

