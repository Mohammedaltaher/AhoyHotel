namespace Application.Model;
public class RoomDto
{
    public int Id { get; set; }
    public int RoomNo { get; set; }
    public int NoOfPersons { get; set; }
    public double Price { get; set; }
    public int HotelId { get; set; }
    public DateTime CreateDate { get; set; }
}

public class RoomModel : BaseModel
{
    public RoomDto Data { get; set; }
}
public class RoomsModel : BaseModel
{
    public List<RoomDto> Data { get; set; }
}


