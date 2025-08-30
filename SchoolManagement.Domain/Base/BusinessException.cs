namespace SchoolManagement.Domain.Base;

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message)
    {
    }
}