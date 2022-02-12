using Application.Features.HotelFeatures.Queries;
using static Application.Features.HotelFeatures.Commands.CreateHotelCommand;
using static Application.Features.HotelFeatures.Queries.GetHotelByIdQuery;
using static Application.Features.HotelFeatures.Commands.UpdateHotelCommand;
using static Application.Features.HotelFeatures.Commands.DeleteHotelByIdCommand;
using static Application.Features.HotelFeatures.Queries.SearchHotelsQuery;

namespace WebApi.Test;
public class HotelTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> MockContext;


    public HotelTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        MockContext = new Mock<IApplicationDbContext>();
        MockContext.Setup(db => db.Hotels).Returns(SharedDatabaseFixture.CreateContext().Hotels);
    }


    [Fact]
    public async Task Can_Get_All_Hotels()
    {
        var handler = new SearchHotelsQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(new SearchHotelsQuery (), CancellationToken.None);
        var Hotel = result.Data;
        Assert.NotNull(Hotel);
        Assert.Equal(HotelData.MockHotelSamples()[1].Name, Hotel[0].Name);
    }


    [Fact]
    public async Task Can_Get_Hotel_By_Id()
    {
        var handler = new GetHotelByIdQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(HotelData.MockGetHotelByIdQuery(), CancellationToken.None);
        var Hotel = result.Data;

        Assert.Equal(HotelData.MockHotelSamples()[0].Name, Hotel.Name);
    }


    [Fact]
    public async Task Can_Add_Hotel()
    {
        var handler = new CreateHotelCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(HotelData.MockCreateHotelCommand(), CancellationToken.None);
        var Hotel = result.Data;

        Assert.Equal(HotelData.MockCreateHotelCommand().Name, Hotel.Name);
    }


    [Fact]
    public async Task Can_Update_Hotel()
    {
        var handler = new UpdateHotelCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(HotelData.MockUpdateHotelCommand(), CancellationToken.None);
        var Hotel =  result.Data;

        Assert.Equal(HotelData.MockUpdateHotelCommand().Name, Hotel.Name);
    }


    [Fact]
    public async Task Can_Delete_Hotel()
    {
        var handler = new DeleteHotelByIdCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(HotelData.MockDeleteHotelByIdCommand(), CancellationToken.None);
        var Hotel =  result.Data;

        Assert.Equal(HotelData.MockHotelSamples()[0].Name, Hotel.Name);
    }
}




