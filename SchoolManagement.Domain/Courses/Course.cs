using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain.Courses;

public class Course : AuditEntity
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public Guid TeacherId { get; private set; }
    
    
    public User Teacher { get; private set; }

    private Course()
    {
    }

    internal Course(string name, string description, Guid teacherId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BusinessException("Course name cannot be empty.");
        
        if (name.Length < 3 || name.Length > 100)
            throw new BusinessException("Course name must be between 3 and 100 characters.");
    
        if (!string.IsNullOrWhiteSpace(description) && description.Length > 500)
            throw new BusinessException("Description is too long.");
        Name = name;
        Description = description;
        TeacherId = teacherId;
    }
    
    internal void Update(string name, string description)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description ?? throw new ArgumentNullException(nameof(description));
    }
    
}