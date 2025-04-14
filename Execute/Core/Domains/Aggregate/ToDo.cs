using Execute.Core.Events;

namespace Execute.Core.Domains.Aggregate;
internal class ToDo : AggregateRoot
{
    public string Title { get; set; } = string.Empty;
    public bool IsCompleted { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public ToDo() { }

    public static ToDo Create(Guid id, string title)
    {
        var todo = new ToDo
        {
            Id = id,
            Title = title
        };
        var @event = new ToDoCreated(id, title);
        todo.AddEvent(@event);
        return todo;
    }

    public void ChangeTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title cannot be empty", nameof(title));
        if (title == Title)
            return;
        Title = title;
        var @event = new ToDoTitleChanged(Id, title);
        AddEvent(@event);
        Apply(@event);
    }

    public void Complete()
    {
        if (IsCompleted)
            return;
        IsCompleted = true;
        var @event = new ToDoCompleted(Id);
        AddEvent(@event);
        Apply(@event);
    }

    public void Delete()
    {
        if (IsCompleted)
            throw new InvalidOperationException("Cannot delete a completed ToDo");
        var @event = new ToDoDeleted(Id);
        AddEvent(@event);
        Apply(@event);
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
            case ToDoDeleted:
                IsDeleted = true;
                break;
            case ToDoCompleted:
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

    public override string ToString()
    {
        return $"ToDo: {Id}, Title: {Title}, Completed: {IsCompleted}, Deleted: {IsDeleted}";
    }
}
