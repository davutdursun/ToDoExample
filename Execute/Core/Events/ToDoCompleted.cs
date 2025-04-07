namespace Execute.Core.Events;
internal record ToDoCompleted(Guid Id) : DomainEvent;