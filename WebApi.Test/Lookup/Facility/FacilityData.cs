using Application.Features.lookup.Commands;
using Application.Features.lookup.Queries;

namespace WebApi.Test.Lookup.Facility;
public static class FacilityData
{
    public static List<Domain.Entities.Facility> MockFacilitySamples() => new()
    {
        new Domain.Entities.Facility()
        {
            Name = "Facility2"
        },
        new Domain.Entities.Facility()
        {
            Name = "Facility2",
        }
    };

    public static GetFacilityByIdQuery MockGetFacilityByIdQuery() => new() { Id = 1 };
    public static CreateFacilityCommand MockCreateFacilityCommand() => new() { Name = "Facility3" };
    public static UpdateFacilityCommand MockUpdateFacilityCommand() => new() { Id = 1, Name = "Facility25" };
    public static DeleteFacilityByIdCommand MockDeleteFacilityByIdCommand() => new() { Id = 2 };

}
