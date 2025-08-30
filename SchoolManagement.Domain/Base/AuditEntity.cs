namespace SchoolManagement.Domain.Base;

public abstract class AuditEntity : Entity
{
    public DateTime CreatedAt { get; set; }

    public Guid? CreatedBy { get; set; }

    public DateTime UpdatedAt { get; set; }

    public Guid? UpdatedBy { get; set; }

    protected AuditEntity()
    {
    }

    protected AuditEntity(Guid id) : base(id)
    {
    }
}
