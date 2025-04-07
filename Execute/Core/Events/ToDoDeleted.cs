using Execute.Domain.Events;

namespace Execute.Core.Events;
internal record ToDoDeleted(int Id) : DomainEvent;