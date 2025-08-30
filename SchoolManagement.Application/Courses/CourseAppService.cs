using AutoMapper;
using AutoMapper.QueryableExtensions;
using SchoolManagement.Application.Base;
using SchoolManagement.Application.Courses.DTOs;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.UnitOfWorks;

namespace SchoolManagement.Application.Courses;

public class CourseAppService : ICourseAppService
{
    private readonly CourseManager _courseManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAsyncQueryableExecutor _asyncQueryableExecutor;
    private readonly ICurrentUser _currentUser;

    public CourseAppService(CourseManager courseManager, IMapper mapper, IUnitOfWork unitOfWork, IAsyncQueryableExecutor asyncQueryableExecutor, ICurrentUser currentUser)
    {
        _courseManager = courseManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _asyncQueryableExecutor = asyncQueryableExecutor;
        _currentUser = currentUser;
    }

    public async Task<Guid> CreateAsync(CreateCourseDto input, CancellationToken cancellationToken = default)
    {
        return await _courseManager.CreateAsync(input.Name, input.Description, input.TeacherId, cancellationToken);
    }

    public async Task<Guid> CreateByTeacherAsync(CreateCourseTeacherDto input, CancellationToken cancellationToken = default)
    {
        var teacherId = _currentUser.UserId?? throw new BusinessException("User not found");
        return await _courseManager.CreateAsync(input.Name, input.Description,teacherId , cancellationToken);
    }

    public async Task UpdateAsync(Guid id ,UpdateCourseDto input, CancellationToken cancellationToken = default)
    {
        await _courseManager.UpdateAsync(id, input.Name, input.Description, cancellationToken);
    }

    public async Task DeleteAsync(Guid courseId, CancellationToken cancellationToken = default)
    {
        await _courseManager.DeleteAsync(courseId, cancellationToken);
    }

    public async Task<PagedResponseDto<CourseDto>> GetAllAsync(CourseRequestDto requestDto, CancellationToken cancellationToken = default)
    {
        var courseQuery = _unitOfWork.CourseRepo.GetQueryable();

        if(requestDto.Name is not null)
            courseQuery = courseQuery.Where(c => c.Name.Contains(requestDto.Name));
        
        var total = await _asyncQueryableExecutor.CountAsync(courseQuery, cancellationToken);

        var dtoCourseQuery = courseQuery
            .Take(requestDto.ResultCount)
            .Skip(requestDto.SkipCount)
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider);

        var courseDtos = await _asyncQueryableExecutor.ToListAsync(dtoCourseQuery, cancellationToken);
        
        return new PagedResponseDto<CourseDto>(total, courseDtos);
    }

    public async Task<PagedResponseDto<CourseDto>> GetByTeacherAsync(
        Guid teacherId, 
        CourseRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var courseQuery = _unitOfWork.CourseRepo.GetQueryable()
            .Where(c => c.TeacherId == teacherId);

        if (!string.IsNullOrWhiteSpace(requestDto.Name))
            courseQuery = courseQuery.Where(c => c.Name.Contains(requestDto.Name));

        var total = await _asyncQueryableExecutor.CountAsync(courseQuery, cancellationToken);

        var dtoCourseQuery = courseQuery
            .Skip(requestDto.SkipCount)
            .Take(requestDto.ResultCount)
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider);

        var courseDtos = await _asyncQueryableExecutor.ToListAsync(dtoCourseQuery, cancellationToken);

        return new PagedResponseDto<CourseDto>(total, courseDtos);
    }


    public async Task<PagedResponseDto<CourseDto>> GetByStudentAsync(
        Guid studentId,
        CourseRequestDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var query = _unitOfWork.EnrollmentRepo.GetQueryable()
            .Where(e => e.StudentId == studentId)
            .Select(e => e.Course); 

        if (!string.IsNullOrWhiteSpace(requestDto.Name))
            query = query.Where(c => c.Name.Contains(requestDto.Name));

        var total = await _asyncQueryableExecutor.CountAsync(query, cancellationToken);

        var dtoCourseQuery = query
            .Skip(requestDto.SkipCount)
            .Take(requestDto.ResultCount)
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider);

        var courseDtos = await _asyncQueryableExecutor.ToListAsync(dtoCourseQuery, cancellationToken);

        return new PagedResponseDto<CourseDto>(total, courseDtos);
    }
    
}