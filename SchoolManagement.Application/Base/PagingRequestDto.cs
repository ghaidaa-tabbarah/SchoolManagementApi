using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Application.Base;

public class PagingRequestDto
{
    private int _resultCount = DefaultMaxResultCount;
    private static int DefaultMaxResultCount { get; set; } = 10;

    private static int MaxResultCount { get; set; } = 1000;

    [Range(1, int.MaxValue)]
    public int ResultCount
    {
        get => _resultCount < MaxResultCount ? _resultCount : MaxResultCount;
        set => _resultCount = value;
    }

    [Range(0, int.MaxValue)] public virtual int SkipCount { get; set; }
}