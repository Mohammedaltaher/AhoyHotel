using Application.Dto;
using Application.Features.lookup.Commands;

namespace Application.Mapper;
public class LookupProfile : Profile
{
    public LookupProfile()
    {
        CreateMap<Facility, CreateFacilityCommand>();
        CreateMap<CreateFacilityCommand, Facility>();
        CreateMap<UpdateFacilityCommand, Hotel>();
        CreateMap<Facility, FacilityDto>();
    }
}

