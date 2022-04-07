using Application.Mapper;
using Microsoft.Extensions.Logging;

namespace WebApi.Test;
public static class MockServices
{
    public static IMapper GetMockedMapper<T>()
    {
        MapperConfiguration mappingConfig = new(profile =>
        {
            profile.AddProfile(new HotelProfile());
            profile.AddProfile(new BookingProfile());
            profile.AddProfile(new LookupProfile());
        });
        return mappingConfig.CreateMapper();
    }
    public static ILogger<T> GetMockedLogger<T>() => new Mock<ILogger<T>>().Object;

}
