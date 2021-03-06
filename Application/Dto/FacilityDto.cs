using Application.Dto.Common;

namespace Application.Dto;
public class FacilityDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Icon { get; set; }
}

public class FacilityModel : BaseModel
{
    public FacilityDto Data { get; set; }
}
public class FacilitiesModel : BaseModel
{
    public List<FacilityDto> Data { get; set; }
}


