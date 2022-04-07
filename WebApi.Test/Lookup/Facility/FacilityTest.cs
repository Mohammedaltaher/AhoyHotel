using Application.Features.FacilityFeatures.Queries;
using static Application.Features.FacilityFeatures.Queries.GetAllFacilityQuery;
using static Application.Features.LookUp.Queries.GetFacilityByIdQuery;
using static Application.Features.LookUp.Commands.CreateFacilityCommand;
using static Application.Features.LookUp.Commands.UpdateFacilityCommand;
using static Application.Features.LookUp.Commands.DeleteFacilityByIdCommand;

namespace WebApi.Test;
public class FacilityTest : IClassFixture<SharedDatabaseFixture>
{
    public SharedDatabaseFixture Fixture { get; }
    private readonly Mock<IApplicationDbContext> MockContext;


    public FacilityTest(SharedDatabaseFixture fixture)
    {
        Fixture = fixture;
        MockContext = new Mock<IApplicationDbContext>();
        MockContext.Setup(db => db.Facilities).Returns(SharedDatabaseFixture.CreateContext().Facilities);
    }


    [Fact]
    public async Task Can_Get_All_Facilitys()
    {
        GetAllFacilityQueryHandler handler = new(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(new GetAllFacilityQuery (), CancellationToken.None);
        List<FacilityDto> Facility = result.Data;
        Assert.NotNull(Facility);
        Assert.Equal(FacilityData.MockFacilitySamples()[0].Name, Facility[0].Name);
    }


    [Fact]
    public async Task Can_Get_Facility_By_Id()
    {
        GetFacilityByIdQueryHandler handler = new(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(FacilityData.MockGetFacilityByIdQuery(), CancellationToken.None);
        var Facility = result.Data;

        Assert.Equal(FacilityData.MockFacilitySamples()[0].Name, Facility.Name);
    }


    [Fact]
    public async Task Can_Add_Facility()
    {
        CreateFacilityCommandHandler handler = new(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(FacilityData.MockCreateFacilityCommand(), CancellationToken.None);
        var Facility = result.Data;

        Assert.Equal(FacilityData.MockCreateFacilityCommand().Name, Facility.Name);
    }


    [Fact]
    public async Task Can_Update_Facility()
    {
        UpdateFacilityCommandHandler handler = new(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(FacilityData.MockUpdateFacilityCommand(), CancellationToken.None);
        var Facility =  result.Data;

        Assert.Equal(FacilityData.MockUpdateFacilityCommand().Name, Facility.Name);
    }


    [Fact]
    public async Task Can_Delete_Facility()
    {
        DeleteFacilityByIdCommandHandler handler = new(MockContext.Object, MockServices.GetMockedMapper<IMapper>());
        var result = await handler.Handle(FacilityData.MockDeleteFacilityByIdCommand(), CancellationToken.None);
        var Facility =  result.Data;

        Assert.Equal(FacilityData.MockFacilitySamples()[0].Name, Facility.Name);
    }
}




