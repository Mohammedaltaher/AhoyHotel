using static Application.Features.Booking.Commands.CreateBookingCommand;

namespace WebApi.Test.Booking;
public class BookingTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> _mockContext;


    public BookingTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        _mockContext = new Mock<IApplicationDbContext>();
        _mockContext.Setup(db => db.Bookings).Returns(SharedDatabaseFixture.CreateContext().Bookings);
    }

    [Fact]
    public async Task Can_Add_Booking()
    {
        CreateBookingCommandHandler  handler = new(_mockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var  result = await handler.Handle(BookingData.MockCreateBookingCommand(), CancellationToken.None);
        var  booking = result.Data;

        Assert.Equal(BookingData.MockCreateBookingCommand().UserName, booking.UserName); 
    }

}




