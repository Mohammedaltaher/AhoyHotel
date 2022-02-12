﻿using Application.Features.HotelFeatures.Commands;
using Application.Features.LookUp.Commands;
using Domain.Entities;

namespace Application.Common.Mapper;
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
