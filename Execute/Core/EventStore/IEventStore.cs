using Execute.Domain.Events;

namespace Execute.Core.EventStore;
internal interface IEventStore
{
    void Save(int aggregateId, IEnumerable<DomainEvent> events);
    List<DomainEvent> GetEvents(int aggregateId);
}
