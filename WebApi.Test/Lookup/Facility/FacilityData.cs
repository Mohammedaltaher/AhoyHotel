using Application.Features.LookUp.Commands;
using Application.Features.LookUp.Queries;

namespace WebApi.Test;
public static class FacilityData
{
    public static List<Facility> MockFacilitySamples()
    {
        var Facility = new List<Facility>()
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

        return Facility;
    }
    public static GetFacilityByIdQuery MockGetFacilityByIdQuery()
    {
        return new GetFacilityByIdQuery()
        {
            Id = 1
        };
    }
    public static CreateFacilityCommand MockCreateFacilityCommand()
    {
        return new CreateFacilityCommand()
        {
            Name = "Facility3",
        };
    }
    public static UpdateFacilityCommand MockUpdateFacilityCommand()
    {
        return new UpdateFacilityCommand()
        {
            Id = 1,
            Name = "Facility25",

        };
    }
    public static DeleteFacilityByIdCommand MockDeleteFacilityByIdCommand()
    {
        return new DeleteFacilityByIdCommand()
        {
            Id = 2
        };
    }

}
