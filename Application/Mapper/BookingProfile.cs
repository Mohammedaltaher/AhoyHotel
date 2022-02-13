using Application.FeaturesBooking.Commands;

namespace Application.Common.Mapper;
public class BookingProfile : Profile
{
    public BookingProfile()
    {
        CreateMap<Booking, CreateBookingCommand>();
        CreateMap<CreateBookingCommand, Booking>();
        CreateMap<Booking, BookingDto>()
         .ForMember(from => from.Room, to => to.MapFrom(value => value.Room))
            ;
    }
}

