using Application.Features.HotelFeatures.Commands;
using Application.Features.HotelFeatures.Queries;

namespace WebApi.Test;
public static class HotelData
{
    public static List<Hotel> MockHotelSamples()
    {
        var Hotel = new List<Hotel>()
            {
                new Hotel()
                {
                    Name = "Payment2",
                },
                new Hotel()
                {
                    Name = "Payment",
                }
            };

        return Hotel;
    }
    public static GetHotelByIdQuery MockGetHotelByIdQuery()
    {
        return new GetHotelByIdQuery()
        {
            Id = 1
        };
    }
    public static CreateHotelCommand MockCreateHotelCommand()
    {
        return new CreateHotelCommand()
        {
            Name = "Payment2",
        };
    }
    public static UpdateHotelCommand MockUpdateHotelCommand()
    {
        return new UpdateHotelCommand()
        {
            Id = 1,
            Name = "Payment25",
        };
    }
    public static DeleteHotelByIdCommand MockDeleteHotelByIdCommand()
    {
        return new DeleteHotelByIdCommand()
        {
            Id = 2
        };
    }

}
