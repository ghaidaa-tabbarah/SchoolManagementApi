using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Application.Assignments;
using SchoolManagement.Application.Auth;
using SchoolManagement.Application.Courses;
using SchoolManagement.Application.Enrollments;
using SchoolManagement.Application.Grades;

namespace SchoolManagement.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IAssignmentAppService, AssignmentAppService>();
        services.AddTransient<ICourseAppService, CourseAppService>();
        services.AddTransient<IGradeAppService, GradeAppService>();
        services.AddTransient<IEnrollmentAppService, EnrollmentAppService>();
        
        services.AddSingleton(
            new MapperConfiguration(config =>
                {
                    config.AddProfile<SchoolManagementMapper>();
                })
                .CreateMapper()
        );
        
        return services;
    }
}