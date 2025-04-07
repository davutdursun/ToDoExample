namespace Execute.Core.Events;
internal record ToDoTitleChanged(Guid Id, string Title) : DomainEvent;