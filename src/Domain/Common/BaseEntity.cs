namespace TaskManagement.Domain.Common;

#nullable disable

public class BaseEntity<T>
{
    public T Id { get; set; }

    public Guid GuidRow { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}
