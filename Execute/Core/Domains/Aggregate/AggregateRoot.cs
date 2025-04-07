using Execute.Domain.Events;

namespace Execute.Core.Domains.Aggregate;
internal abstract class AggregateRoot : Domain
{
    private readonly List<DomainEvent> _domainEvents = [];
    public IReadOnlyList<DomainEvent> Events => _domainEvents.AsReadOnly();
    protected void AddEvent(DomainEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearEvents() => _domainEvents.Clear();
}
