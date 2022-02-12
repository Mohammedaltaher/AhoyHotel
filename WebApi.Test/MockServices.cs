using Application.Common.Mapper;
namespace WebApi.Test;
public static class MockServices
{
    public static IMapper GetMockedMapper<T>()
    {
        var mappingConfig = new MapperConfiguration(profile =>
        {
            profile.AddProfile(new HotelProfile());
        });
        var moq = mappingConfig.CreateMapper();
        return moq;
    }

}
