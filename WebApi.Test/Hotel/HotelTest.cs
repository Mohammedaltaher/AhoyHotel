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
    public async Task Can_Get_All_HotelsAsync()
    {
        var handler = new SearchHotelsQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(new SearchHotelsQuery (), CancellationToken.None);
        var Hotel = result.Data;
        Assert.NotNull(Hotel);
        Assert.Equal(HotelData.MockHotelSamples()[1].Name, Hotel[0].Name);
        Assert.Equal(2, Hotel.Count);
    }


    [Fact]
    public async Task Can_Get_Hotel_By_IdAsync()
    {
        var handler = new GetHotelByIdQueryHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(HotelData.MockGetHotelByIdQuery(), CancellationToken.None);
        var Hotel = result.Data;

        Assert.Equal("Payment2", Hotel.Name);
    }


    [Fact]
    public async Task Can_Add_HotelAsync()
    {
        var handler = new CreateHotelCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(HotelData.MockCreateHotelCommand(), CancellationToken.None);
        var Hotel = result.Data;

        Assert.Equal("Payment2", Hotel.Name);
    }


    [Fact]
    public async Task Can_Update_HotelAsync()
    {
        var handler = new UpdateHotelCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(HotelData.MockUpdateHotelCommand(), CancellationToken.None);
        var Hotel =  result.Data;

        Assert.Equal("Payment25", Hotel.Name);
    }


    [Fact]
    public async Task Can_Delete_HotelAsync()
    {
        var handler = new DeleteHotelByIdCommandHandler(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(HotelData.MockDeleteHotelByIdCommand(), CancellationToken.None);
        var Hotel =  result.Data;

        Assert.Equal("Payment", Hotel.Name);
    }
}




