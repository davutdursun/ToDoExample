using Execute.Domain.Events;

namespace Execute.Core.EventStore;
internal class InMemoryEventStore : IEventStore
{
    private readonly Dictionary<int, List<DomainEvent>> _store = [];
    public List<DomainEvent> GetEvents(int aggregateId)
    {
        return _store.TryGetValue(aggregateId, out var events) ? events : [];
    }

    public void Save(int aggregateId, IEnumerable<DomainEvent> events)
    {
        if (!_store.ContainsKey(aggregateId))
            _store[aggregateId] = [];

        _store[aggregateId].AddRange(events);
    }
}
