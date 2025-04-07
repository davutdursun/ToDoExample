using Execute.Core.Events;

namespace Execute.Core.EventStore;
internal class InMemoryEventStore : IEventStore
{
    private readonly Dictionary<Guid, List<DomainEvent>> _store = [];
    public List<DomainEvent> GetEvents(Guid aggregateId)
    {
        return _store.TryGetValue(aggregateId, out var events) ? events : [];
    }

    public void Save(Guid aggregateId, IEnumerable<DomainEvent> events)
    {
        if (!_store.ContainsKey(aggregateId))
            _store[aggregateId] = [];

        _store[aggregateId].AddRange(events);
    }
}
