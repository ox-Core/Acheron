using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Acheron.Core.ECS;

public class SystemManager {
    private readonly Dictionary<string, System> systems = [];
    private readonly Dictionary<Entity, Signature> entitySignatures = [];
    private readonly Dictionary<string, Stage> stages = [];
    private readonly List<Stage> stageOrder = [];

    private bool isStarted = false;

    public System Register(string name, Delegate func, Signature? signature = null, string stageName = "Update") {
        var system = new System(name, func, signature ?? new Signature());
        systems[name] = system;
        
        
        GetStageOrFail(stageName).Systems.Add(system);

        foreach (var kv in entitySignatures)
            UpdateSystemForEntity(system, kv.Key, kv.Value);

        return system;
    }

    public void SetEntitySignature(Entity entity, Signature signature) {
        entitySignatures[entity] = signature;
        foreach (var system in systems.Values)
            UpdateSystemForEntity(system, entity, signature);
    }

    public void StageBefore(string before, string after) {
        int index = stageOrder.FindIndex(s => s.Name == after);
        if (index == -1) index = stageOrder.Count;
        GetOrCreateStage(before, index);
    }

    public void StageAfter(string after, string before) {
        int index = stageOrder.FindIndex(s => s.Name == before);
        if (index == -1) index = stageOrder.Count;
        else index += 1;

        GetOrCreateStage(after, index);
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
        else system.Entities.Remove(entity);
    }

    public Stage GetOrCreateStage(string name, int? insertIndex = null) {
        if (!stages.TryGetValue(name, out var stage)) {
            stage = new Stage(name);
            stages[name] = stage;

            if (insertIndex.HasValue && insertIndex.Value >= 0 && insertIndex.Value <= stageOrder.Count)
                stageOrder.Insert(insertIndex.Value, stage);
            else stageOrder.Add(stage);
        }
        return stage;
    }

    public Stage GetStageOrFail(string name) {
        if (!stages.TryGetValue(name, out var stage)) {
            throw new InvalidOperationException("Failed to get stage '" + name + "'");
        }

        return stage;
    }
    
    private void PopulateAttributes(World world) {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var asm in assemblies) {
            foreach (var type in asm.GetTypes()) {
                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {
                    var sysAttr = method.GetCustomAttribute<SystemAttribute>();
                    if (sysAttr != null) {
                        var parameters = method.GetParameters().Select(p => p.ParameterType).ToList();
                        var funcType = Expression.GetActionType(parameters.ToArray());
                        var func = method.CreateDelegate(funcType);

                        var signature = new Signature([.. sysAttr.Components.Select(t => world.GetComponentID(t))]);
                        Register(method.Name, func, signature, sysAttr.Stage);
                    }
                }
            }
        }
    }

    public void UpdateAllStages(World world, double dt) {
        foreach (var stage in stages.Values) {
            if (stage.Name == "Start" && isStarted) return;
            else if (stage.Name == "Start" && !isStarted) PopulateAttributes(world);

            foreach (var system in stage.Systems) {
                system.Update(world, dt);
            }
        }
        isStarted = true;
    }
}