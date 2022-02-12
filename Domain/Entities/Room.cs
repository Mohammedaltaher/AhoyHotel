namespace Domain.Entities;
public class Room : BaseEntity
{
    public int RoomNo { get; set; }
    public int NoOfPersons { get; set; }
    public double Price { get; set; }
    public int HotelId { get; set; }
    public virtual Hotel Hotel { get; set; }
}
