using System.Security.Claims;
using SchoolManagement.Domain.Base;

namespace SchoolManagement.Presentation.API;

public class CurrentUserService : ICurrentUser 
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    public Guid? UserId
    {
        get
        {
            var value = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
            
            if (value != null)
                return Guid.Parse(value);
            
            return null;
        }
    }
}