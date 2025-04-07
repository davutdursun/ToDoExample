namespace Execute.Domain.Events;
internal record ToDoCreated(int Id, string Title) : DomainEvent;