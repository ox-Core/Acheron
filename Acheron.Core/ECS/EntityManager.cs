namespace Acheron.Core.ECS;

public class EntityManager {
    private ulong idCounter = 1;

    private readonly List<Entity> availableEntities = [];
    private readonly Dictionary<Entity, Signature> signatures = [];

    public Entity Spawn() {
        Entity entity;
        if (availableEntities.Count > 0) {
            entity = availableEntities[^1];
            availableEntities.RemoveAt(availableEntities.Count - 1);
        } else {
            entity = new Entity(idCounter++);
        }

        signatures[entity] = [];

        return entity;
    }

    public void Despawn(Entity entity) {
        if (!signatures.ContainsKey(entity))
            throw new InvalidOperationException("Entity doesnt exist.");

        signatures[entity].Clear();
        signatures.Remove(entity);
        availableEntities.Add(entity);
    }

    public void SetSignature(Entity entity, Signature signature) {
        if (!signatures.ContainsKey(entity))
            throw new InvalidOperationException("Entity doesnt exist.");

        signatures[entity] = signature;
    }

    public Signature GetSignature(Entity entity) {
        if (!signatures.TryGetValue(entity, out var signature))
            throw new InvalidOperationException("Entity doesnt exist.");

        return signature;
    }
}