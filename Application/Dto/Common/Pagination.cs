namespace Application.Dto.Common;
public class Pagination
{
    private const int MaxPageSize = 50;
    private int _pageSize = 10;

    public int PageSize
    {
        get => _pageSize;
        set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
    public int PageNumber { get; set; } = 1;
}
