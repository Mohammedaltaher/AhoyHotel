namespace Application.Model;
public class ReviewDto
{
    public int Id { get; set; }
    public string RevieweName { get; set; }
    public string ReviewerEmail { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public int HotelId { get; set; }
    public DateTime CreateDate { get; set; }
}

public class ReviewModel : BaseModel
{
    public ReviewDto Data { get; set; }
}
public class ReviewsModel : BaseModel
{
    public List<ReviewDto> Data { get; set; }
}


