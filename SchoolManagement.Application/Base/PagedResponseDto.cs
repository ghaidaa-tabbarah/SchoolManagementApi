namespace SchoolManagement.Application.Base;

public class PagedResponseDto<T>
{
    public long TotalCount { get; set; }
    public IReadOnlyList<T> Items { get; set; }


    public PagedResponseDto(int totalCount, List<T> items)
    {
        TotalCount = totalCount;
        Items = items.AsReadOnly();
    }
    
    public PagedResponseDto(long totalCount, IReadOnlyList<T> items)
    {
        TotalCount = totalCount;
        Items = items;
    }
}