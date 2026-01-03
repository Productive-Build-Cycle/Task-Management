namespace TaskManagement.Domain.Common;

#nullable disable

public abstract class BaseEntity<T>
{
    #region Properties

    public T Id { get; set; }

    public Guid GuidRow { get; set; } = Guid.NewGuid();

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? UpdatedDate { get; set; }

    public bool IsDeleted { get; set; }

    #endregion Properties

    #region Methods

    public void MarkAsUpdated()
    {
        UpdatedDate = DateTime.Now;
    }

    public void MarkAsDeleted()
    {
        IsDeleted = true;

        MarkAsUpdated();
    }

    #endregion Methods
}
