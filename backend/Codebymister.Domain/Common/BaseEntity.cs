namespace Codebymister.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public DateTime? DeletedAt { get; private set; }
    public Guid? DeletedBy { get; private set; }
    public bool IsDeleted => DeletedAt.HasValue;

    public void SetCreatedAt()
    {
        var now = DateTime.UtcNow;
        CreatedAt = now;
        UpdatedAt = now;
    }

    public void SetUpdatedAt()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void SoftDelete(Guid? deletedBy = null)
    {
        if (IsDeleted)
            return;

        DeletedAt = DateTime.UtcNow;
        DeletedBy = deletedBy;
    }

    public void Restore()
    {
        if (!IsDeleted)
            return;

        DeletedAt = null;
        DeletedBy = null;
    }
}
