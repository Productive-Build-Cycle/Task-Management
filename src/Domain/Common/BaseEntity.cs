namespace TaskManagement.Domain.Common;

#nullable disable

public abstract class BaseEntity<T>
{
    public T Id { get; set; }

    public Guid GuidRow { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; set; } = DateTime.Now;
    public DateTime UpdatedDate { get; set; } = DateTime.Now;
    public bool IsDeleted { get; set; }
    public void MarkAsUpdated()
    {
        UpdatedDate = DateTime.Now;
    }
    public void MarkAsDeleted()
    {
        IsDeleted = true;
        MarkAsUpdated();
    }
}
