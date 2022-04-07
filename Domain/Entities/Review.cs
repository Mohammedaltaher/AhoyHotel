namespace Domain.Entities;
public class Review : BaseEntity
{
    public string ReviewName { get; set; }
    public string ReviewerEmail { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }

    public int HotelId { get; set; }
    public virtual Hotel Hotel { get; set; }
}
