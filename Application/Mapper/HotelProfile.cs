using Application.Features.HotelFeatures.Commands;
using Domain.Entities;

namespace Application.Common.Mapper;
public class HotelProfile : Profile
{
    public HotelProfile()
    {
        CreateMap<Hotel, CreateHotelCommand>();
        CreateMap<CreateHotelCommand, Hotel>();
        CreateMap<UpdateHotelCommand, Hotel>();
        CreateMap<Hotel, HotelDto>()
         .ForMember(from => from.Rooms, to => to.MapFrom(value => value.Rooms))
         .ForMember(from => from.Reviews, to => to.MapFrom(value => value.Reviews))
         .ForMember(from => from.Images, to => to.MapFrom(value => value.Images))
         .ForMember(from => from.Facilities, to => to.MapFrom(value => value.HotelFacilities))
         ;
        CreateMap<Review, ReviewDto>();
        CreateMap<Room, RoomDto>();
        CreateMap<HotelImage, HotelImageDto>();
        CreateMap<HotelFacility, FacilityDto >()
               .ForMember(from => from.Name, to => to.MapFrom(value => value.Facility.Name))
               .ForMember(from => from.Icon, to => to.MapFrom(value => value.Facility.Icon))
            ;
    }
}

