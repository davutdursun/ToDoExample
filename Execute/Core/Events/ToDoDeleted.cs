namespace Execute.Core.Events;
internal record ToDoDeleted(Guid Id) : DomainEvent;