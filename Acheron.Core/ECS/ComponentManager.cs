using Acheron.Core.ECS.Internal;

namespace Acheron.Core.ECS;

public class ComponentManager {
    private ushort nextComponentID = 0;

    private readonly Dictionary<ComponentID, object> componentArrays = [];
    private readonly Dictionary<ComponentID, string> componentNames = [];

    private readonly Dictionary<Type, ComponentID> typeToID = [];

    public ComponentID RegisterComponent<T>() {
        var type = typeof(T);
        if (typeToID.ContainsKey(type))
            throw new InvalidOperationException($"Compoennt {type.Name} already registered.");

        var id = nextComponentID++;
        typeToID[type] = id;

        componentArrays[id] = new ComponentArray<T>();
        componentNames[id] = type.Name;

        return id;
    }

    public ComponentID GetComponentID<T>() {
        var type = typeof(T);

        if (!typeToID.TryGetValue(type, out var id))
            throw new InvalidOperationException($"Component {type.Name} not registered.");

        return id;
    }

    public ComponentID GetComponentID(Type t) => typeToID[t];

    public void EntityDespawned(Entity entity) {
        foreach(var arr in componentArrays.Values) {
            ((IComponentArray)arr).EntityDespawned(entity);
        }
    }

    public void AddComponent<T>(Entity entity, T component) {
        GetComponentArray<T>().InsertData(entity, component);
    }

    public void RemoveComponent<T>(Entity entity) {
        GetComponentArray<T>().RemoveData(entity);
    }

    public ref T GetComponent<T>(Entity entity) {
        return ref GetComponentArray<T>().GetData(entity);
    }

    public bool HasComponent<T>(Entity entity) {
        return GetComponentArray<T>().HasData(entity);
    }

    public string GetComponentName(ComponentID id) {
        return componentNames.TryGetValue(id, out var name) ? name : "???";
    }

    private ComponentArray<T> GetComponentArray<T>() {
        var id = GetComponentID<T>();

        if (!componentArrays.TryGetValue(id, out var array))
            throw new InvalidOperationException($"Component array for {typeof(T).Name} not found.");

        return (ComponentArray<T>)array;
    }
}