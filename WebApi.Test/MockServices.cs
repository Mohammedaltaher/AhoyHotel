using Application.Mapper;
using Microsoft.Extensions.Logging;

namespace WebApi.Test;
public static class MockServices
{
    public static IMapper GetMockedMapper<I>()
    {
        MapperConfiguration mappingConfig = new(profile =>
        {
            profile.AddProfile(new HotelProfile());
            profile.AddProfile(new BookingProfile());
            profile.AddProfile(new LookupProfile());
        });
        return mappingConfig.CreateMapper();
    }
    public static ILogger<T> GetMockedLoger<T>() => new Mock<ILogger<T>>().Object;

}
