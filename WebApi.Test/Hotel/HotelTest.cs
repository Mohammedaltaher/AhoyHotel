using Application.Dto;
using Application.Features.Hotel.Commands;
using Application.Features.Hotel.Queries;
using static Application.Features.Hotel.Commands.CreateHotelCommand;
using static Application.Features.Hotel.Queries.GetHotelByIdQuery;
using static Application.Features.Hotel.Commands.UpdateHotelCommand;
using static Application.Features.Hotel.Commands.DeleteHotelByIdCommand;
using static Application.Features.Hotel.Queries.SearchHotelsQuery;

namespace WebApi.Test.Hotel;
public class HotelTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> _mockContext;


    public HotelTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        _mockContext = new Mock<IApplicationDbContext>();
        _mockContext.Setup(db => db.Hotels).Returns(SharedDatabaseFixture.CreateContext().Hotels);
    }


    [Fact]
    public async Task Can_Get_All_Hotels()
    {
        SearchHotelsQueryHandler  handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>(), MockServices.GetMockedLogger<SearchHotelsQuery>());
        var  result = await handler.Handle(new SearchHotelsQuery(), CancellationToken.None);
        List<HotelDto>  hotel = result.Data;
        Assert.NotNull(hotel);
        Assert.Equal(HotelData.MockHotelSamples()[1].Name, hotel[0].Name);
    }


    [Fact]
    public async Task Can_Get_Hotel_By_Id()
    {
        GetHotelByIdQueryHandler  handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var  result = await handler.Handle(HotelData.MockGetHotelByIdQuery(), CancellationToken.None);
        var  hotel = result.Data;

        Assert.Equal(HotelData.MockHotelSamples()[0].Name, hotel.Name);
    }


    [Fact]
    public async Task Can_Add_Hotel()
    {
        CreateHotelCommandHandler  handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>(),MockServices.GetMockedLogger<CreateHotelCommand>());
        var  result = await handler.Handle(HotelData.MockCreateHotelCommand(), CancellationToken.None);
        var  hotel = result.Data;

        Assert.Equal(HotelData.MockCreateHotelCommand().Name, hotel.Name);
    }


    [Fact]
    public async Task Can_Update_Hotel()
    {
        UpdateHotelCommandHandler  handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>(), MockServices.GetMockedLogger<UpdateHotelCommand>());
        var  result = await handler.Handle(HotelData.MockUpdateHotelCommand(), CancellationToken.None);
        var  hotel = result.Data;

        Assert.Equal(HotelData.MockUpdateHotelCommand().Name, hotel.Name);
    }


    [Fact]
    public async Task Can_Delete_Hotel()
    {
        DeleteHotelByIdCommandHandler  handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>(), MockServices.GetMockedLogger<DeleteHotelByIdCommand>());
        var  result = await handler.Handle(HotelData.MockDeleteHotelByIdCommand(), CancellationToken.None);
        var  hotel = result.Data;

        Assert.Equal(HotelData.MockHotelSamples()[0].Name, hotel.Name);
    }
}




