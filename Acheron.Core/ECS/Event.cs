namespace Acheron.Core.ECS;

interface IEventQueue { void Dispatch(World world); }

class EventQueue<T> : IEventQueue {
    private readonly List<T> events = new();
    private readonly List<Action<World, T>> subs = [];

    public void Subscribe(Action<World, T> callback) => subs.Add(callback);
    public void Emit(T ev) => events.Add(ev);

    public void Dispatch(World world) {
        foreach (var e in events)
            foreach (var s in subs)
                s(world, e);
        events.Clear();
    }
}

public class EventManager {
    private readonly Dictionary<Type, IEventQueue> queues = [];

    public void Subscribe<T>(Action<World, T> callback) {
        if (!queues.TryGetValue(typeof(T), out var q))
            queues[typeof(T)] = q = new EventQueue<T>();
        ((EventQueue<T>)q).Subscribe(callback);
    }

    public void Emit<T>(T ev) {
        if (!queues.TryGetValue(typeof(T), out var q))
            queues[typeof(T)] = q = new EventQueue<T>();
        ((EventQueue<T>)q).Emit(ev);
    }

    public void Dispatch(World world) {
        foreach (var q in queues.Values)
            q.Dispatch(world);
    }
}