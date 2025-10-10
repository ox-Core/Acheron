namespace Acheron.Core.ECS;

public class SystemManager {
    private readonly Dictionary<string, System> systems = [];
    private readonly Dictionary<Entity, Signature> entitySignatures = [];
    private readonly Dictionary<string, Stage> stages = [];

    private ulong systemCounter = 0;

    public System Register(Delegate func, Signature? signature = null, string stageName = "Update") {
        string name = $"System_{systemCounter++}";

        var system = new System(func, signature ?? new Signature());
        systems[name] = system;
        
        GetOrCreateStage(stageName).Systems.Add(system);

        foreach (var kv in entitySignatures)
            UpdateSystemForEntity(system, kv.Key, kv.Value);

        return system;
    }

    public void SetEntitySignature(Entity entity, Signature signature) {
        entitySignatures[entity] = signature;
        foreach (var system in systems.Values)
            UpdateSystemForEntity(system, entity, signature);
    }

    public void EntityDespawned(Entity entity) {
        entitySignatures.Remove(entity);
        foreach(var system in systems.Values) {
            system.Entities.Remove(entity);
        }
    }

    private void UpdateSystemForEntity(System system, Entity entity, Signature signature) {
        if (system.Matches(signature))
            system.Entities.Add(entity);
        else
            system.Entities.Remove(entity);
    }

    private Stage GetOrCreateStage(string name) {
        if (!stages.TryGetValue(name, out var stage)) {
            stage = new Stage(name);
            stages[name] = stage;
        }
        return stage;
    }
    
    public void UpdateAllStages(World world, double dt) {
        foreach (var stage in stages.Values)
            foreach (var system in stage.Systems)
                system.Update(world, dt);
    }
}