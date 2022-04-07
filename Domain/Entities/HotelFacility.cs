namespace Domain.Entities;
public class HotelFacility : BaseEntity
{
    public int FacilityId { get; set; }
    public virtual Facility Facility { get; set; }
    public int HotelId { get; set; }
    public virtual Hotel Hotel { get; set; }
}
