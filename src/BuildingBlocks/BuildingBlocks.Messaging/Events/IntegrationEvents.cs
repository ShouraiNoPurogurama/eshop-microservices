namespace BuildingBlocks.Messaging.Events;

public record IntegrationEvents
{
    public Guid Id => Guid.NewGuid();

    public DateTime OccurredOn => DateTime.Now;

    public string EventType => GetType().AssemblyQualifiedName;
}