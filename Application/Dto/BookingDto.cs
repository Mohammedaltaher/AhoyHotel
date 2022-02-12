namespace Application.Model;
public class BookingDto
{
    public int Id { get; set; }
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public bool IsConfirmed { get; set; }
    public double ActualPrice { get; set; }
    public double Discount { get; set; }
    public double PaidAmount { get; set; }
    public string UserName { get; set; }
    public int RoomId { get; set; }
    public RoomDto Room { get; set; }
    public DateTime UpdateDate { get; set; }
}

public class BookingModel : BaseModel
{
    public BookingDto Data { get; set; }
}
public class BookingsModel : BaseModel
{
    public List<BookingDto> Data { get; set; }
}


