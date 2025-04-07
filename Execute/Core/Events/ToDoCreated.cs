namespace Execute.Core.Events;
internal record ToDoCreated(Guid Id, string Title) : DomainEvent;