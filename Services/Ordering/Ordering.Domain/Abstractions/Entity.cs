namespace Ordering.Domain.Abstractions;

//Interface Segregation Pattern
public abstract class Entity<T> : IEntity<T>
{
    public T Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastModified { get; set; }
    public string? LastModifiedBy { get; set; }
    public short? Act_Ind { get; set; } = 1;
    public short? Del_Ind { get; set; } = 0;
}
