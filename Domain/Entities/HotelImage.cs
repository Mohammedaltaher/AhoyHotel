namespace Domain.Entities;
public class HotelImage : BaseEntity
{
    public string Url { get; set; }
    public int HotelId { get; set; }
    public virtual Hotel Hotel { get; set; }
}
