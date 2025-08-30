using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain.Security;

public interface ITokenGenerator
{
    string GenerateToken(User user);
}