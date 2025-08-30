using System.Security.Cryptography;
using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Domain;

public class PasswordHelper
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public void SetPassword(User user, string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new BusinessException("Password cannot be empty.");

        using var rng = RandomNumberGenerator.Create();
        var saltBytes = new byte[SaltSize];
        rng.GetBytes(saltBytes);
        user.SetPasswordSalt(Convert.ToBase64String(saltBytes));

        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize
        );

        user.SetPasswordHash(Convert.ToBase64String(hashBytes));
    }

    public bool CheckPassword(User user, string password)
    {
        if (string.IsNullOrWhiteSpace(user.PasswordSalt) || string.IsNullOrWhiteSpace(user.PasswordHash))
            throw new BusinessException("Password is not set.");

        var saltBytes = Convert.FromBase64String(user.PasswordSalt);

        var hashBytes = Rfc2898DeriveBytes.Pbkdf2(
            password,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize
        );

        return Convert.ToBase64String(hashBytes) == user.PasswordHash;
    }
}