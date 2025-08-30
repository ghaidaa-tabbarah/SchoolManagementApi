using SchoolManagement.Application.Auth.DTOs;
using SchoolManagement.Domain.Security;
using SchoolManagement.Domain.Users;

namespace SchoolManagement.Application.Auth;

public class AuthService : IAuthService
{
    private readonly ITokenGenerator _tokenGenerator;
    private readonly UserManager _userManager;

    public AuthService(ITokenGenerator tokenGenerator, UserManager userManager)
    {
        _tokenGenerator = tokenGenerator;
        _userManager = userManager;
    }
    
    public async Task<string> LoginAsync(LoginRequestDto requestDto, CancellationToken cancellationToken)
    {
        var user = await _userManager.LoginAsync(requestDto.UserName, requestDto.Password, cancellationToken);

        return _tokenGenerator.GenerateToken(user);
    }
    
    
    public async Task RegisterAsync(RegisterUserDto dto, CancellationToken ct = default)
    {
        var fullName = new FullName(dto.FirstName, dto.LastName, dto.FatherName);

        await _userManager.RegisterAsync(fullName, dto.Username, dto.Password, dto.Role, ct);
    }

}