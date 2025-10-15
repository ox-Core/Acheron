namespace Acheron.Core.ECS;

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