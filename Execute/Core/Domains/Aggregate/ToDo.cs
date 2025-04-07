using Execute.Core.Events;
using Execute.Domain.Events;

namespace Execute.Core.Domains.Aggregate;
internal class ToDo : AggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public ToDo() { }

    public static ToDo Create(int id, string title)
    {
        var todo = new ToDo
        {
            Id = id,
            Title = title
        };
        todo.AddEvent(new ToDoCreated(id, title));
        return todo;
    }

    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        if (title == Title)
            return;
        Title = title;
        AddEvent(new ToDoTitleChanged(Id, title));
    }

    public void Complete()
    {
        if (IsCompleted)
            return;
        IsCompleted = true;
        AddEvent(new ToDoCompleted(Id));
    }

    public void Delete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Cannot delete a completed ToDo");
        AddEvent(new ToDoDeleted(Id));
    }

    public void Apply(DomainEvent @event)
    {
        switch (@event)
        {
            case ToDoCreated e:
                Id = e.Id;
                Title = e.Title;
                break;
            case ToDoTitleChanged e:
                Title = e.Title;
                break;
            case ToDoDeleted e:
                IsDeleted = true;
                break;
            case ToDoCompleted e:
                IsCompleted = true;
                break;
        }
    }

    public static ToDo Load(IEnumerable<DomainEvent> events)
    {
        var todo = new ToDo();
        foreach (var @event in events)
        {
            todo.Apply(@event);
        }
        return todo;
    }
}
