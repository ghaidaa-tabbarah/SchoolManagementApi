using SchoolManagement.Domain.Base;
using SchoolManagement.Domain.UnitOfWorks;

namespace SchoolManagement.Domain.Users;

public class UserManager
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAsyncQueryableExecutor _asyncExecutor;
    private readonly PasswordHelper _passwordHelper;

    public UserManager(IUnitOfWork unitOfWork, IAsyncQueryableExecutor asyncExecutor, PasswordHelper passwordHelper)
    {
        _unitOfWork = unitOfWork;
        _asyncExecutor = asyncExecutor;
        _passwordHelper = passwordHelper;
    }

    public async Task RegisterAsync(FullName name, string username, string password, UserRole role,
        CancellationToken ct = default)
    {
        var query = _unitOfWork.UserRepo.GetQueryable()
            .Where(u => u.Username == username);

        var exists = await _asyncExecutor.AnyAsync(query, ct);
        if (exists)
            throw new BusinessException("Username already exists.");

        if (role is UserRole.Admin)
            throw new BusinessException("You cannot register as admin user.");
        
        var user = new User(name, username, password, role, _passwordHelper);

        await _unitOfWork.UserRepo.AddAsync(user);
        await _unitOfWork.SaveChangesAsync(ct);
    }

    public async Task<User> LoginAsync(string username, string password, CancellationToken ct = default)
    {
        var query = _unitOfWork.UserRepo.GetQueryable()
            .Where(u => u.Username == username);

        var user = await _asyncExecutor.FirstOrDefaultAsync(query, ct);

        if (user == null || !user.CheckPassword(password , _passwordHelper))
            throw new BusinessException("Invalid username or password.");

        return user;
    }
}
