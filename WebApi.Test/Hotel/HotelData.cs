using Application.Features.HotelFeatures.Commands;
using Application.Features.HotelFeatures.Queries;

namespace WebApi.Test;
public static class HotelData
{
    public static List<Hotel> MockHotelSamples() => new()
    {
        new Hotel()
        {
            Name = "Hotel2",
            PhoneNumber = "0585199391",
            Email = "email@gmail.com",
            Address = "Dubai",
            Description = "good hotel",
        },
        new Hotel()
        {
            Name = "Hotel",
            PhoneNumber = "0584875391",
            Email = "m@gmail.com",
            Address = "address",
            Description = " hotel",
        }
    };
    public static GetHotelByIdQuery MockGetHotelByIdQuery() => new() { Id = 1 };

    public static CreateHotelCommand MockCreateHotelCommand() => new()
    {
        Name = "Hotel3",
        PhoneNumber = "0584875391",
        Email = "m@gmail.com",
        Address = "address",
        Description = " hotel",
    };
    public static UpdateHotelCommand MockUpdateHotelCommand() => new()
    {
        Id = 1,
        Name = "Hotel25",
        PhoneNumber = "0584875391",
        Email = "m@gmail.com",
        Address = "address",
        Description = " hotel",

    };
    public static DeleteHotelByIdCommand MockDeleteHotelByIdCommand() => new() { Id = 2 };

}
