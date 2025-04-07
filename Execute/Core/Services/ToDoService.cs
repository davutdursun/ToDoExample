using Execute.Core.Domains.Aggregate;
using Execute.Core.Events;
using Execute.Core.EventStore;

namespace Execute.Core.Services;
internal class ToDoService(IEventStore eventStore)
{
    private readonly IEventStore _eventStore = eventStore;

    public Guid CreateToDo(string title)
    {
        var id = Guid.NewGuid();
        var todo = ToDo.Create(id, title);
        _eventStore.Save(id, todo.Events);
        return id;
    }
    public void ChangeToDoTitle(Guid id, string title)
    {
        var events = _eventStore.GetEvents(id);
        var todo = ToDo.Load(events);
        todo.ChangeTitle(title);
        _eventStore.Save(id, todo.Events);
    }
    public void DeleteToDo(Guid id)
    {
        var events = _eventStore.GetEvents(id);
        var todo = ToDo.Load(events);
        todo.Delete();
        _eventStore.Save(id, todo.Events);
    }
    public void CompleteToDo(Guid id)
    {
        var events = _eventStore.GetEvents(id);
        var todo = ToDo.Load(events);
        todo.Complete();
        _eventStore.Save(id, todo.Events);
    }
    public IEnumerable<DomainEvent> GetHistory(Guid aggregateId)
    {
        return _eventStore.GetEvents(aggregateId);
    }
}