using Execute.Core.EventStore;
using Execute.Core.Services;

var store = new InMemoryEventStore();
var service = new ToDoService(store);

var id = service.CreateToDo("Learn C#");

service.ChangeToDoTitle(id, "Learn C# and DDD");

service.DeleteToDo(id);

var id2 = service.CreateToDo("Learn C# and DDD");

service.CompleteToDo(id2);

foreach (var @event in service.GetHistory(id))
{
    Console.WriteLine($"{@event.EventId} - {@event.Timestamp} - {@event.GetType()}");
}

Console.WriteLine("\n\n");

foreach (var @event in service.GetHistory(id2))
{
    Console.WriteLine($"{@event.EventId} - {@event.Timestamp} - {@event.GetType()}");
}