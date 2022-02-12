namespace Application.Model;
public class HotelImageDto
{
    public int Id { get; set; }
    public string Url { get; set; }
    public int HotelId { get; set; }
    public DateTime CreateDate { get; set; }
}

public class HotelImageModel : BaseModel
{
    public HotelImageDto Data { get; set; }
}
public class HotelImagesModel : BaseModel
{
    public List<HotelImageDto> Data { get; set; }
}


