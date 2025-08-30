namespace SchoolManagement.Domain.Base;

public interface ICurrentUser
{
    Guid? UserId { get; }
}