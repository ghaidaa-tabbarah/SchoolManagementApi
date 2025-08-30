using SchoolManagement.Domain.Users;

namespace SchoolManagement.Application.Auth.DTOs;

public class RegisterUserDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; } 
    public string FatherName { get; set; } 
    public string Username { get; set; }
    public string Password { get; set; } 
    public UserRole Role { get; set; }
}