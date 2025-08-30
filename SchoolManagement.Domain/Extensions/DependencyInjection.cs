using Microsoft.Extensions.DependencyInjection;
using SchoolManagement.Domain.Assignments;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Enrollments;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        services.AddTransient<AssignmentManager>();
        services.AddTransient<CourseManager>();
        services.AddTransient<EnrollmentManger>();
        services.AddTransient<UserManager>();
        services.AddTransient<PasswordHelper>();
        return services;
    }
}