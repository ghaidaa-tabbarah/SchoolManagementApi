
using SchoolManagement.Application.Auth.DTOs;

namespace SchoolManagement.Application.Auth;

public interface IAuthService
{
    public Task<string> LoginAsync(LoginRequestDto requestDto, CancellationToken cancellationToken);
    public Task RegisterAsync(RegisterUserDto dto, CancellationToken cancellationToken);
}