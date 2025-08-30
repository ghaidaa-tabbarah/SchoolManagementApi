using SchoolManagement.Domain.Base;

namespace SchoolManagement.Domain.Users;

public class FullName
{
    public string FirstName { get; private set; }
    public string FatherName { get; private set; }
    public string LastName { get; private set; }

    private FullName()
    {
    }
    
    public FullName(string firstName, string fatherName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new BusinessException("First name cannot be empty");
        if (string.IsNullOrWhiteSpace(fatherName))
            throw new BusinessException("Father name cannot be empty");
        if (string.IsNullOrWhiteSpace(lastName))
            throw new BusinessException("Last name cannot be empty");

        if (firstName.Length > 50 || fatherName.Length > 50 || lastName.Length > 50)
            throw new BusinessException("Names are too long");

        FirstName = firstName.Trim();
        FatherName = fatherName.Trim();
        LastName = lastName.Trim();
    }

    public string GetFullName() => $"{FirstName} {FatherName} {LastName}";

    public override string ToString() => GetFullName();

    public override bool Equals(object? obj) =>
        obj is FullName other &&
        FirstName == other.FirstName &&
        FatherName == other.FatherName &&
        LastName == other.LastName;

    public override int GetHashCode() => HashCode.Combine(FirstName, FatherName, LastName);
}
