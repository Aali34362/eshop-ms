namespace BaseIntegrated.BaseEvents;

public interface IBaseDomainEvent : INotification{ }

public abstract class BaseDomainEvent(string message) : IBaseDomainEvent
{
    public string Message { get; } = message;
}