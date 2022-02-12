using Domain.Common;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;
public class Booking : BaseEntity
{
    public DateTime CheckIn { get; set; }
    public DateTime CheckOut { get; set; }
    public bool IsConfirmed { get; set; }
    public double ActualPrice { get; set; }
    public double Discount { get; set; }
    public double PaidAmount { get; set; }
    public string UserName { get; set; }
    public int RoomId { get; set; }
    public Room Room { get; set; }
}
