using Application.Features.Hotel.Commands;
using Application.Features.Hotel.Queries;

namespace WebApi.Test.Hotel;
public static class HotelData
{
    public static List<Domain.Entities.Hotel> MockHotelSamples() => new()
    {
        new Domain.Entities.Hotel()
        {
            Name = "Hotel2",
            PhoneNumber = "0585199391",
            Email = "email@gmail.com",
            Address = "Dubai",
            Description = "good hotel",
        },
        new Domain.Entities.Hotel()
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
