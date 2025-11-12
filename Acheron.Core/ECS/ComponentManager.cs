using Acheron.Core.ECS.Internal;

namespace Acheron.Core.ECS;


static class ComponentCache<T> {
    public static ComponentArray<T>? Array;
    public static ComponentID ID;
    public static bool Initialized;
}

public class ComponentManager {
    private ushort nextComponentID = 0;

    private const int MAX_COMPONENT_COUNT = 4096;
    private readonly object?[] componentArrays = new object?[MAX_COMPONENT_COUNT];
    private readonly Dictionary<ComponentID, string> componentNames = [];

    private readonly Dictionary<Type, ComponentID> typeToID = [];
    private readonly Dictionary<ComponentID, Type> idToType = [];

    public ComponentID RegisterComponent<T>() {
        var type = typeof(T);
        if (typeToID.ContainsKey(type))
            throw new InvalidOperationException($"Component {type.Name} already registered.");

        var id = nextComponentID++;

        typeToID[type] = id;
        idToType[id] = type;
        componentArrays[id] = new ComponentArray<T>();
        componentNames[id] = type.Name;

        ComponentCache<T>.ID = id;
        ComponentCache<T>.Array = (ComponentArray<T>)componentArrays[id]!;
        ComponentCache<T>.Initialized = true;
        return id;
    }

    public void EntityDespawned(Entity entity) {
        for (int i = 0; i < nextComponentID; i++) {
            var arr = componentArrays[i];
            if (arr != null)
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
        return ref ComponentCache<T>.Array!.GetData(entity);
    }

    public Type GetComponentType(ComponentID id) {
        if (!idToType.TryGetValue(id, out var type))
            throw new InvalidOperationException($"ComponentID {id} not registered.");
        return type;
    }

    public bool HasComponent<T>(Entity entity) {
        return GetComponentArray<T>().HasData(entity);
    }

    public ComponentID GetComponentID<T>() {
        return ComponentCache<T>.ID;
    }

    public ComponentID GetComponentID(Type type) {
        if (typeToID.TryGetValue(type, out var id))
            return id;

        throw new InvalidOperationException($"Component type {type.FullName} not registered.");
    }
    
    public string GetComponentName(ComponentID id) {
        return componentNames.TryGetValue(id, out var name) ? name : "???";
    }

    private ComponentArray<T> GetComponentArray<T>() {
        if (!ComponentCache<T>.Initialized || ComponentCache<T>.Array == null)
            throw new InvalidOperationException($"Component {typeof(T).Name} not registered.");
        return ComponentCache<T>.Array;
    }
}