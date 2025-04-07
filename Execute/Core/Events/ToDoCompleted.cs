namespace Execute.Domain.Events;
internal record ToDoCompleted(int Id) : DomainEvent;