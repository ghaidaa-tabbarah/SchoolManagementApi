using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchoolManagement.Application.Base;
using SchoolManagement.Application.Grades.DTOs;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.UnitOfWorks;

namespace SchoolManagement.Application.Grades;

public class GradeAppService : IGradeAppService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAsyncQueryableExecutor _asyncQueryableExecutor;
    private readonly IMapper _mapper;

    public GradeAppService(IUnitOfWork unitOfWork, IAsyncQueryableExecutor asyncQueryableExecutor, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _asyncQueryableExecutor = asyncQueryableExecutor;
        _mapper = mapper;
    }

    public async Task<PagedResponseDto<GradeDto>> GetAllAsync(
        GradeRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var gradeQuery = _unitOfWork.GradeRepo.GetQueryable();

        if (requestDto.MinValue.HasValue)
            gradeQuery = gradeQuery.Where(g => g.Value >= requestDto.MinValue.Value);

        if (requestDto.MaxValue.HasValue)
            gradeQuery = gradeQuery.Where(g => g.Value <= requestDto.MaxValue.Value);

        if (requestDto.AssignmentId.HasValue)
            gradeQuery = gradeQuery.Where(g => g.AssignmentId == requestDto.AssignmentId.Value);

        var total = await _asyncQueryableExecutor.CountAsync(gradeQuery, cancellationToken);

        if (!string.IsNullOrWhiteSpace(requestDto.SortBy))
        {
            if (requestDto.SortBy.Equals("gradedat", StringComparison.OrdinalIgnoreCase))
                gradeQuery = requestDto.SortDesc
                    ? gradeQuery.OrderByDescending(g => g.GradedAt)
                    : gradeQuery.OrderBy(g => g.GradedAt);
            else if (requestDto.SortBy.Equals("value", StringComparison.OrdinalIgnoreCase))
                gradeQuery = requestDto.SortDesc
                    ? gradeQuery.OrderByDescending(g => g.Value)
                    : gradeQuery.OrderBy(g => g.Value);
        }
        else
        {
            gradeQuery = gradeQuery.OrderBy(g => g.Value);
        }

        var dtoQuery = gradeQuery
            .Skip(requestDto.SkipCount)
            .Take(requestDto.ResultCount)
            .ProjectTo<GradeDto>(_mapper.ConfigurationProvider);

        var gradeDtos = await _asyncQueryableExecutor.ToListAsync(dtoQuery, cancellationToken);

        return new PagedResponseDto<GradeDto>(total, gradeDtos);
    }
}