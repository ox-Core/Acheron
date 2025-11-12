using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

namespace Acheron.Core.ECS;

public class SystemManager {
    private readonly Dictionary<string, ISystem> systems = [];
    private readonly Dictionary<Entity, Signature> entitySignatures = [];
    private readonly Dictionary<string, Stage> stages = [];
    private readonly List<Stage> stagesOrdered = [];

    private bool isStarted = false;

    public void Register(string name, ISystem system, Signature? signature = null, string stageName = "Update") {
        systems[name] = system;
        
        if(DoesStageExist(stageName))
            GetStageOrFail(stageName).Systems.Add(system);

        foreach (var kv in entitySignatures)
            UpdateSystemForEntity(system, kv.Key, kv.Value);
    }

    public void SetEntitySignature(Entity entity, Signature signature) {
        entitySignatures[entity] = signature;
        foreach (var system in systems.Values)
            UpdateSystemForEntity(system, entity, signature);
    }

    public void StageBefore(string before, string after) {
        int index = stagesOrdered.FindIndex(s => s.Name == after);
        if (index == -1) index = stagesOrdered.Count;
        GetOrCreateStage(before, index);
    }

    public void StageAfter(string after, string before) {
        int index = stagesOrdered.FindIndex(s => s.Name == before);
        if (index == -1) index = stagesOrdered.Count;
        else index += 1;

        GetOrCreateStage(after, index);
    }

    public void EntityDespawned(Entity entity) {
        entitySignatures.Remove(entity);
        foreach(var system in systems.Values) {
            system.Entities.Remove(entity);
        }
    }

    private void UpdateSystemForEntity(ISystem system, Entity entity, Signature signature) {
        if (system.Matches(signature))
            system.Entities.Add(entity);
        else system.Entities.Remove(entity);
    }

    public Stage GetOrCreateStage(string name, int? insertIndex = null) {
        if (!stages.TryGetValue(name, out var stage)) {
            stage = new Stage(name);
            stages[name] = stage;

            if (insertIndex.HasValue && insertIndex.Value >= 0 && insertIndex.Value <= stagesOrdered.Count)
                stagesOrdered.Insert(insertIndex.Value, stage);
            else stagesOrdered.Add(stage);
        }
        return stage;
    }

    public Stage GetStageOrFail(string name) {
        if (!stages.TryGetValue(name, out var stage)) {
            throw new InvalidOperationException("Failed to get stage '" + name + "'");
        }

        return stage;
    }

    public bool DoesStageExist(string name) {
        return stages.ContainsKey(name);
    }

    private void PopulateAttributes(World world) {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach (var asm in assemblies) {
            foreach (var type in asm.GetTypes()) {
                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)) {
                    var sysAttr = method.GetCustomAttribute<SystemAttribute>();

                    if (sysAttr != null) {
                        var attrType = sysAttr.GetType();
                        var componentTypes = attrType.IsGenericType
                            ? attrType.GetGenericArguments()
                            : Type.EmptyTypes;
                        var signature = new Signature([.. componentTypes.Select(t => world.GetComponentID(t))]);
                        var func = SystemHelper.GetSystemType(componentTypes, signature, method);
                        Register(method.Name, func, signature, sysAttr.Stage);
                    }
                }
            }
        }
    }

    public void UpdateAllStages(World world) {
        foreach (var stage in stagesOrdered) {
            if (stage.Name == "Start" && isStarted) continue;
            else if (stage.Name == "Start" && !isStarted) PopulateAttributes(world);

            foreach (var system in stage.Systems) {
                system.Update(world);
            }
        }
        isStarted = true;
    }
}