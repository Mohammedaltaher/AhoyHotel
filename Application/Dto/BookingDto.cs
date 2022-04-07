using Application.Dto.Common;

namespace Application.Dto;
public class BookingDto
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public RoomDto Room { get; set; }
}

public class BookingModel : BaseModel
{
    public BookingDto Data { get; set; }
}
