using Application.Common.Mapper;
using Application.Features.HotelFeatures.Queries;
using Microsoft.Extensions.Logging;

namespace WebApi.Test;
public static class MockServices
{
    public static IMapper GetMockedMapper<T>()
    {
        var mappingConfig = new MapperConfiguration(profile =>
        {
            profile.AddProfile(new HotelProfile());
            profile.AddProfile(new BookingProfile());
            profile.AddProfile(new LookupProfile());
        });
        var moq = mappingConfig.CreateMapper();
        return moq;
    }
    public static ILogger<T> GetMockedLoger<T>()
    {
        return Mock.Of<ILogger<T>>();
    }

}
