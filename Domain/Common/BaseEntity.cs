namespace Domain.Common;
public abstract class BaseEntity
{
    public BaseEntity()
    {
        CreateDate = DateTime.Now;
        UpdateDate = DateTime.Now;
        IsDeleted = false;
    }
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
