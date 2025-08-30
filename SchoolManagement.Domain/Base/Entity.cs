namespace SchoolManagement.Domain.Base;

public abstract class Entity
{
    public Guid Id { get; protected set; }
    public bool IsDeleted { get; set; }

    protected Entity()
    {
        Id = Guid.NewGuid();
    }

    protected Entity(Guid id)
    {
        Id = id;
    }


    public override string ToString()
    {
        return GetType().Name + " [Id=" + Id + "]";
    }
}