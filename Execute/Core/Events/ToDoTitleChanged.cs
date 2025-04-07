namespace Execute.Domain.Events;
internal record ToDoTitleChanged(int Id, string Title) : DomainEvent;