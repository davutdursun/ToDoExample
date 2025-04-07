using Execute.Core.Domains.Aggregate;
using Execute.Core.EventStore;
using Execute.Domain.Events;

namespace Execute.Core.Services;
internal class ToDoService(IEventStore eventStore)
{
    private readonly IEventStore _eventStore = eventStore;

    public int CreateToDo(string title)
    {
        var id = 0;
        var todo = ToDo.Create(id, title);
        _eventStore.Save(id, todo.Events);
        return id;
    }
    public void ChangeToDoTitle(int id, string title)
    {
        var events = _eventStore.GetEvents(id);
        var todo = ToDo.Load(events);
        todo.ChangeTitle(title);
        _eventStore.Save(id, todo.Events);
    }
    public void DeleteToDo(int id)
    {
        var events = _eventStore.GetEvents(id);
        var todo = ToDo.Load(events);
        todo.Delete();
        _eventStore.Save(id, todo.Events);
    }
    public void CompleteToDo(int id)
    {
        var events = _eventStore.GetEvents(id);
        var todo = ToDo.Load(events);
        todo.Complete();
        _eventStore.Save(id, todo.Events);
    }
    public IEnumerable<DomainEvent> GetHistory(int aggregateId)
    {
        return _eventStore.GetEvents(aggregateId);
    }
}