using Microsoft.Extensions.Logging;
using SchoolManagement.Domain.Courses;
using SchoolManagement.Domain.Users;
using SchoolManagement.Domain.Assignments;
using SchoolManagement.Domain.Enrollments;
using SchoolManagement.Domain.Grades;
using System.Reflection;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.UnitOfWorks;

namespace SchoolManagement.Domain.DataSeeders;

public class DataSeeder
{
    private readonly ILogger<DataSeeder> _logger;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAsyncQueryableExecutor _asyncQueryableExecutor;
    private readonly PasswordHelper _passwordHelper;


    public DataSeeder(ILogger<DataSeeder> logger, IUnitOfWork unitOfWork,
        IAsyncQueryableExecutor asyncQueryableExecutor, PasswordHelper passwordHelper)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
        _asyncQueryableExecutor = asyncQueryableExecutor;
        _passwordHelper = passwordHelper;
    }

    public async Task SeedAsync()
    {
        try
        {
            _logger.LogInformation("Starting data seeding process...");

            await SeedUsers();
            await SeedCourses();
            await SeedAssignments();
            await SeedEnrollments();
            await SeedGrades();

            _logger.LogInformation("Data seeding completed successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during data seeding");
            throw;
        }
    }

    private async Task SeedUsers()
    {
        var isUserExist = await _asyncQueryableExecutor.AnyAsync(
            _unitOfWork.UserRepo.GetQueryable()
        );

        if (isUserExist)
            return;

        var users = new List<User>
        {
            new User(
                new FullName("Admin", "Admin", "Admin"),
                "Admin",
                "Admin",
                UserRole.Admin, _passwordHelper),

            new User(
                new FullName("Jane", "Elizabeth", "Smith"),
                "jane.smith",
                "Password123!",
                UserRole.Teacher, _passwordHelper),

            new User(
                new FullName("Mike", "Robert", "Johnson"),
                "mike.johnson",
                "Password123!",
                UserRole.Student, _passwordHelper),
            new User(
                new FullName("Sarah", "Anne", "Wilson"),
                "sarah.wilson",
                "Password123!",
                UserRole.Student, _passwordHelper),
        };

        foreach (var user in users)
        {
            await _unitOfWork.UserRepo.AddAsync(user);
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Seeded {Count} users", users.Count);
    }

    private async Task SeedCourses()
    {
        var isCoursesExist = await _asyncQueryableExecutor.AnyAsync(
            _unitOfWork.CourseRepo.GetQueryable()
        );

        if (isCoursesExist)
            return;

        var teacherId = await _asyncQueryableExecutor.FirstAsync(
            _unitOfWork.UserRepo.GetQueryable()
                .Where(u => u.Role == UserRole.Teacher)
                .Select(u => u.Id)
        );

        var courses = new List<Course>
        {
            new Course("Mathematics", "Advanced mathematics including algebra, calculus, and geometry", teacherId),
            new Course("English Literature", "Study of classic and contemporary English literature", teacherId),
            new Course("Computer Science", "Introduction to programming and computer science fundamentals", teacherId),
            new Course("History", "World history from ancient civilizations to modern times", teacherId)
        };

        foreach (var course in courses)
        {
            await _unitOfWork.CourseRepo.AddAsync(course);
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Seeded {Count} courses", courses.Count);
    }

    private async Task SeedAssignments()
    {
        var isAssignmentsExist = await _asyncQueryableExecutor.AnyAsync(
            _unitOfWork.AssignmentRepo.GetQueryable()
        );

        if (isAssignmentsExist)
            return;

        var studentId = await _asyncQueryableExecutor.FirstAsync(
            _unitOfWork.UserRepo.GetQueryable()
                .Where(u => u.Role == UserRole.Student)
                .Select(u => u.Id)
        );

        var courseId = await _asyncQueryableExecutor.FirstAsync(
            _unitOfWork.CourseRepo.GetQueryable()
                .Where(c => c.Name == "Mathematics")
                .Select(c => c.Id)
        );

        var assignments = new List<Assignment>
        {
            new Assignment(courseId, studentId, DateTime.UtcNow.AddDays(7), 
                "Complete problems 1-20 in Chapter 3 of Algebra"),

            new Assignment(courseId, studentId, DateTime.UtcNow.AddDays(10), 
                "Write a 1000-word essay on any Shakespeare play"),

            new Assignment(courseId, studentId, DateTime.UtcNow.AddDays(14), 
                "Create a simple calculator application using C#")
        };

        foreach (var assignment in assignments)
        {
            await _unitOfWork.AssignmentRepo.AddAsync(assignment);
        }

        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Seeded {Count} assignments", assignments.Count);
    }

    private async Task SeedEnrollments()
    {
        var isEnrollmentsExist = await _asyncQueryableExecutor.AnyAsync(
            _unitOfWork.EnrollmentRepo.GetQueryable()
        );

        if (isEnrollmentsExist)
            return;

        var studentId = await _asyncQueryableExecutor.FirstAsync(
            _unitOfWork.UserRepo.GetQueryable()
                .Where(u => u.Role == UserRole.Student)
                .Select(u => u.Id)
        );

        var mathCourseId = await _asyncQueryableExecutor.FirstAsync(
            _unitOfWork.CourseRepo.GetQueryable()
                .Where(c => c.Name == "Mathematics")
                .Select(c => c.Id)
        );

        var englishCourseId = await _asyncQueryableExecutor.FirstAsync(
            _unitOfWork.CourseRepo.GetQueryable()
                .Where(c => c.Name == "English Literature")
                .Select(c => c.Id)
        );

        var enrollments = new List<Enrollment>
        {
            new Enrollment(studentId, mathCourseId),
            new Enrollment(studentId, englishCourseId)
        };

        foreach (var enrollment in enrollments)
        {
            await _unitOfWork.EnrollmentRepo.AddAsync(enrollment);
        }
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Seeded {Count} enrollments", enrollments.Count);
    }


    private async Task SeedGrades()
    {
        var isGradesExist = await _asyncQueryableExecutor.AnyAsync(
            _unitOfWork.GradeRepo.GetQueryable()
        );

        if (isGradesExist)
            return;

        var assignmentId = await _asyncQueryableExecutor.FirstAsync(
            _unitOfWork.AssignmentRepo.GetQueryable().Select(a => a.Id)
        );

        var grades = new List<Grade>
        {
            new Grade(assignmentId, 85, DateTime.UtcNow.AddDays(-5), "Good work on the algebra problems"),
            new Grade(assignmentId, 92, DateTime.UtcNow.AddDays(-3), "Excellent essay on Shakespeare")
        };

        foreach (var grade in grades)
        {
            await _unitOfWork.GradeRepo.AddAsync(grade);
        }
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("Seeded {Count} grades", grades.Count);
    }
}