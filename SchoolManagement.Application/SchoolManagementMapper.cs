using AutoMapper;
using SchoolManagement.Application.Courses.DTOs;
using SchoolManagement.Application.Grades;
using SchoolManagement.Application.Grades.DTOs;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Grades;

namespace SchoolManagement.Application;

public class SchoolManagementMapper : Profile
{
    public SchoolManagementMapper()
    {
        CreateMap<Course, CourseDto>()
            .ForMember(dest => dest.TeacherName, opt => opt.MapFrom(src => src.Teacher.Name.ToString()));
        
        CreateMap<Grade, GradeDto>();
    }
}