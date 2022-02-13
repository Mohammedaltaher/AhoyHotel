using static Application.FeaturesBooking.Commands.CreateBookingCommand;

namespace WebApi.Test;
public class BookingTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> MockContext;


    public BookingTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        MockContext = new Mock<IApplicationDbContext>();
        MockContext.Setup(db => db.Bookings).Returns(SharedDatabaseFixture.CreateContext().Bookings);
    }

    [Fact]
    public async Task Can_Add_Booking()
    {
        CreateBookingCommandHandler  handler = new(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        BookingModel  result = await handler.Handle(BookingData.MockCreateBookingCommand(), CancellationToken.None);
        BookingDto  Booking = result.Data;

        Assert.Equal(BookingData.MockCreateBookingCommand().UserName, Booking.UserName);
    }

}




