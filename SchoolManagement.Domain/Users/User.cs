using SchoolManagement.Domain.Base;

namespace SchoolManagement.Domain.Users;

public class User : AuditEntity
{
    public FullName Name { get; private set; }
    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public UserRole Role { get; private set; }

    private User()
    {
    }

    internal User(FullName name, string username, string password, UserRole role, PasswordHelper passwordHelper)
    {
        Name = name ?? throw new BusinessException("FullName cannot be null.");

        if (string.IsNullOrWhiteSpace(username) || username.Length < 3 || username.Length > 50)
            throw new BusinessException("Username must be between 3 and 50 characters.");

        Username = username;
        Role = role;

        passwordHelper.SetPassword(this, password);
    }

    public bool CheckPassword(string password , PasswordHelper passwordHelper)
    {
        return passwordHelper.CheckPassword(this, password);
    }

    internal void SetPasswordHash(string hash) => PasswordHash = hash;
    internal void SetPasswordSalt(string salt) => PasswordSalt = salt;
}