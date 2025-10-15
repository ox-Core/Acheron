namespace Acheron.Core.ECS;

interface IEventQueue { void Dispatch(World world); }

class EventQueue<T> : IEventQueue {
    private readonly List<T> events = [];
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