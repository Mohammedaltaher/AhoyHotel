using Application.Features.LookUp.Commands;
using Application.Features.LookUp.Queries;

namespace WebApi.Test;
public static class FacilityData
{
    public static List<Facility> MockFacilitySamples() => new()
    {
        new Facility()
        {
            Name = "Facility2"
        },
        new Facility()
        {
            Name = "Facility",
        }
    };

    public static GetFacilityByIdQuery MockGetFacilityByIdQuery() => new() { Id = 1 };
    public static CreateFacilityCommand MockCreateFacilityCommand() => new() { Name = "Facility3" };
    public static UpdateFacilityCommand MockUpdateFacilityCommand() => new() { Id = 1, Name = "Facility25" };
    public static DeleteFacilityByIdCommand MockDeleteFacilityByIdCommand() => new() { Id = 2 };

}
