namespace Acheron.Core.ECS.Internal;

public interface IComponentArray {
    void EntityDespawned(Entity entity);
}

public class ComponentArray<T> : IComponentArray {
    private T[] componentArray = [];
    private int size = 0;

    private readonly Dictionary<Entity, int> entityToIndex = [];
    private readonly Dictionary<int, Entity> indexToEntity = [];

    private void EnsureCapacity() {
        if (size >= componentArray.Length) {
            int newCapacity = (componentArray.Length + 1) * 2;
            Array.Resize(ref componentArray, newCapacity);
        }
    }

    public void InsertData(Entity entity, T component) {
        if (entityToIndex.ContainsKey(entity))
            throw new InvalidOperationException("Duplicate Components on Entity.");

        EnsureCapacity();

        int newIndex = size;
        entityToIndex[entity] = newIndex;
        indexToEntity[newIndex] = entity;
        componentArray[newIndex] = component;

        size++;
    }

    public void SetData(Entity entity, T component) {
        if (!entityToIndex.TryGetValue(entity, out int index))
            throw new InvalidOperationException("Settings nonexistant component");

        componentArray[index] = component;
    }

    public bool HasData(Entity entity) {
        return entityToIndex.ContainsKey(entity);
    }

    public void RemoveData(Entity entity) {
        if (!entityToIndex.TryGetValue(entity, out int indexOfRemoved))
            throw new InvalidOperationException("Removal called for Component that doesnt exist.");

        int indexOfLast = size - 1;

        componentArray[indexOfRemoved] = componentArray[indexOfLast];

        Entity entityOfLast = indexToEntity[indexOfLast];
        entityToIndex[entityOfLast] = indexOfRemoved;
        indexToEntity[indexOfRemoved] = entityOfLast;

        entityToIndex.Remove(entity);
        indexToEntity.Remove(indexOfLast);

        size--;
    }

    public ref T GetData(Entity entity) {
        if (!entityToIndex.TryGetValue(entity, out int index))
            throw new InvalidOperationException("Trying to get Component that doesnt exist.");

        return ref componentArray[index];
    }

    public object GetDataObject(Entity entity) {
        if (!entityToIndex.TryGetValue(entity, out int index))
            throw new InvalidOperationException("Trying to get Component that doesnt exist.");
        return componentArray[index]!;
    }

    public void EntityDespawned(Entity entity) {
        if (entityToIndex.ContainsKey(entity))
            RemoveData(entity);
    }
}