namespace Application.Dto;
public class RoomDto
{
    public int Id { get; set; }
    public int RoomNo { get; set; }
    public int NoOfPersons { get; set; }
    public double Price { get; set; }
    public int HotelId { get; set; }
    public HotelDto Hotel { get; set; }
    public DateTime CreateDate { get; set; }
}



