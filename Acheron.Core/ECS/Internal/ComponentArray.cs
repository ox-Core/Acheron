namespace Acheron.Core.ECS.Internal;

public interface IComponentArray {
    void EntityDespawned(Entity entity);
}

public class ComponentArray<T> : IComponentArray {
    private T[] componentArray = [];
    private int size = 0;

    private int[] sparse = [];
    private Entity[] dense = [];

    private void EnsureCapacity() {
        if (size >= componentArray.Length) {
            int newSize = (componentArray.Length + 1) * 2;
            Array.Resize(ref componentArray, newSize);
            Array.Resize(ref dense, newSize);
        }
    }

    private void EnsureSparseCapacity(int entityId) {
        if (entityId >= sparse.Length) {
            int oldSize = sparse.Length;
            int newSize = Math.Max(entityId + 1, (sparse.Length + 1) * 2);
            Array.Resize(ref sparse, newSize);
            Array.Fill(sparse, -1, oldSize, newSize - oldSize);
        }
    }

    public void InsertData(Entity entity, T component) {
        var entityId = (int)entity.Value;
        EnsureSparseCapacity(entityId);

        if (sparse[entityId] != -1)
            throw new InvalidOperationException("Duplicate Components on Entity.");

        EnsureCapacity();

        sparse[entityId] = size;
        dense[size] = entity;
        componentArray[size] = component;
        size++;
    }

    public void SetData(Entity entity, T component) {
        var entityId = (int)entity.Value;
        if (entityId >= sparse.Length || sparse[entityId] == -1)
            throw new InvalidOperationException("Setting nonexistent component");

        componentArray[sparse[entityId]] = component;
    }

    public bool HasData(Entity entity) {
        var entityId = (int)entity.Value;
        return entityId < sparse.Length && sparse[entityId] != -1;
    }

    public void RemoveData(Entity entity) {
        var entityId = (int)entity.Value;
        if (entityId >= sparse.Length || sparse[entityId] == -1)
            throw new InvalidOperationException("Removal called for Component that doesn't exist.");

        var indexOfRemoved = sparse[entityId];
        var indexOfLast = size - 1;

        componentArray[indexOfRemoved] = componentArray[indexOfLast];
        dense[indexOfRemoved] = dense[indexOfLast];

        sparse[dense[indexOfRemoved].Value] = indexOfRemoved;

        sparse[entityId] = -1;
        size--;
    }

    public ref T GetData(Entity entity) {
        int entityId = (int)entity.Value;
        if (entityId >= sparse.Length || sparse[entityId] == -1)
            throw new InvalidOperationException($"Trying to get Component '{typeof(T).Name}', but it doesn't exist.");

        return ref componentArray[sparse[entityId]];
    }

    public void EntityDespawned(Entity entity) {
        int entityId = (int)entity.Value;
        if (entityId < sparse.Length && sparse[entityId] != -1)
            RemoveData(entity);
    }
}