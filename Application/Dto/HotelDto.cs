using Application.Dto.Common;

namespace Application.Dto;
public class HotelDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual List<FacilityDto> Facilities { get; set; }
    public virtual List<HotelImageDto> Images { get; set; }
    public virtual List<RoomDto> Rooms { get; set; }
    public virtual List<ReviewDto> Reviews { get; set; }
}

public class HotelModel : BaseModel
{
    public HotelDto Data { get; set; }
}
public class HotelsModel : BaseModel
{
    public List<HotelDto> Data { get; set; }
}


