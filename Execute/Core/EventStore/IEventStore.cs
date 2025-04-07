using Execute.Core.Events;

namespace Execute.Core.EventStore;
internal interface IEventStore
{
    void Save(Guid aggregateId, IEnumerable<DomainEvent> events);
    List<DomainEvent> GetEvents(Guid aggregateId);
}
