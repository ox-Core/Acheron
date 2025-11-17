using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;

using Acheron.Core.ECS.Internal;

namespace Acheron.Core.ECS;

public class World {
    private EntityManager entityManager = new();
    private SystemManager systemManager = new();
    private ComponentManager componentManager = new();
    private EventManager eventManager = new();

    private readonly Stopwatch dtStopwatch = Stopwatch.StartNew();
    private double dtLastTime = 0;

    private double deltaTime = 0;

    public World() {
        systemManager.GetOrCreateStage("Start");
        systemManager.StageAfter("PreUpdate", "Start");
        systemManager.StageAfter("Update", "PreUpdate");
        systemManager.StageAfter("PostUpdate", "Update");
        PopulateAttributes();
    }

    private void PopulateAttributes() {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies();
        foreach(var asm in assemblies) {
            foreach (var type in asm.GetTypes()) {
                if (type.GetCustomAttributes(typeof(ComponentAttribute), true).Length > 0) {
                    RegisterComponent(type);
                }

                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic)) {                    
                    var subAttr = method.GetCustomAttribute<UntypedSubscribeAttribute>();
                    if (subAttr != null) {
                        Type eventType = subAttr.EventType!;

                        var parameters = method.GetParameters().Select(p => p.ParameterType).ToArray();
                        var funcType = Expression.GetActionType(parameters);
                        var func = method.CreateDelegate(funcType);

                        var subscribeMethod = typeof(EventManager)
                            .GetMethods()
                            .FirstOrDefault(m => m.Name == "Subscribe" && m.IsGenericMethodDefinition)!;

                        var genericSubscribe = subscribeMethod.MakeGenericMethod(eventType);
                        genericSubscribe.Invoke(eventManager, [func]);
                    }
                }
            }
        }
    }

    public Entity Spawn() => entityManager.Spawn();

    public Entity SpawnWith<T1>(T1 c1) {
        var e = Spawn();
        AddComponent(e, c1);
        return e;
    }

    public Entity SpawnWith<T1, T2>(T1 c1, T2 c2) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3>(T1 c1, T2 c2, T3 c3) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3, T4>(T1 c1, T2 c2, T3 c3, T4 c4) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3, T4, T5>(T1 c1, T2 c2, T3 c3, T4 c4, T5 c5) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        AddComponent(e, c5);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3, T4, T5, T6>(T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        AddComponent(e, c5);
        AddComponent(e, c6);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3, T4, T5, T6, T7>(T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6, T7 c7) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        AddComponent(e, c5);
        AddComponent(e, c6);
        AddComponent(e, c7);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3, T4, T5, T6, T7, T8>(T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6, T7 c7, T8 c8) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        AddComponent(e, c5);
        AddComponent(e, c6);
        AddComponent(e, c7);
        AddComponent(e, c8);
        return e;
    }

    public IEnumerable<Entity> Entities => entityManager.Entities;

    public Entity SpawnWith<T1, T2, T3, T4, T5, T6, T7, T8, T9>(T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6, T7 c7, T8 c8, T9 c9) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        AddComponent(e, c5);
        AddComponent(e, c6);
        AddComponent(e, c7);
        AddComponent(e, c8);
        AddComponent(e, c9);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6, T7 c7, T8 c8, T9 c9, T10 c10) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        AddComponent(e, c5);
        AddComponent(e, c6);
        AddComponent(e, c7);
        AddComponent(e, c8);
        AddComponent(e, c9);
        AddComponent(e, c10);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6, T7 c7, T8 c8, T9 c9, T10 c10, T11 c11) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        AddComponent(e, c5);
        AddComponent(e, c6);
        AddComponent(e, c7);
        AddComponent(e, c8);
        AddComponent(e, c9);
        AddComponent(e, c10);
        AddComponent(e, c11);
        return e;
    }

    public Entity SpawnWith<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(T1 c1, T2 c2, T3 c3, T4 c4, T5 c5, T6 c6, T7 c7, T8 c8, T9 c9, T10 c10, T11 c11, T12 c12) {
        var e = Spawn();
        AddComponent(e, c1);
        AddComponent(e, c2);
        AddComponent(e, c3);
        AddComponent(e, c4);
        AddComponent(e, c5);
        AddComponent(e, c6);
        AddComponent(e, c7);
        AddComponent(e, c8);
        AddComponent(e, c9);
        AddComponent(e, c10);
        AddComponent(e, c11);
        AddComponent(e, c12);
        return e;
    }

    public void Despawn(Entity entity) {
        entityManager.Despawn(entity);
        systemManager.EntityDespawned(entity);
        componentManager.EntityDespawned(entity);
    }

    public void RegisterComponent<T>() => componentManager.RegisterComponent<T>();

    public void RegisterComponent(Type t) {
        var methodInfo = typeof(ComponentManager).GetMethod(nameof(ComponentManager.RegisterComponent));
        if (methodInfo == null)
            throw new InvalidOperationException("SOMETHING VERY BAD HAS HAPPENED");
        var method = methodInfo.MakeGenericMethod(t);
        method.Invoke(componentManager, null);
    }

    public bool HasComponent<T>(Entity entity) => componentManager.HasComponent<T>(entity);
    
    public ComponentID GetComponentID(Type t) => componentManager.GetComponentID(t);
    public ComponentID GetComponentID<T>() => componentManager.GetComponentID<T>();

    public void AddComponent<T>(Entity entity, T component) {
        componentManager.AddComponent<T>(entity, component);

        var signature = entityManager.GetSignature(entity);
        signature.Add(componentManager.GetComponentID<T>());

        entityManager.SetSignature(entity, signature);
        systemManager.SetEntitySignature(entity, signature);
    }

    public void RemoveComponent<T>(Entity entity) {
        componentManager.RemoveComponent<T>(entity);

        var signature = entityManager.GetSignature(entity);
        signature.Remove(componentManager.GetComponentID<T>());

        entityManager.SetSignature(entity, signature);
        systemManager.SetEntitySignature(entity, signature);
    }

    public ref T GetComponent<T>(Entity entity) => ref componentManager.GetComponent<T>(entity);

    public Type GetComponentType(ComponentID id) => componentManager.GetComponentType(id);

    public void SetSingleton<T>(T instance) where T : new() {
        SingletonStorage<T>.Set(instance);
    }

    public ref T GetSingleton<T>() where T : new() {
        return ref SingletonStorage<T>.Get();
    }

    public bool IsSingletonSet<T>() where T : new() => SingletonStorage<T>.IsSet;   

    public void AddStageBefore(string before, string after) => systemManager.StageBefore(before, after);
    public void AddStageAfter(string after, string before) => systemManager.StageAfter(after, before);

    public void ImportModule<T>() where T : Module, new() => new T().RegisterWithDeps(this);

    public double DeltaTime => deltaTime;

    public void Emit<T>(T ev) => eventManager.Emit<T>(ev);

    public void View<T1>(ViewFunc<T1> view) => new View<T1>(view).Invoke(this);
    public void View<T1, T2>(ViewFunc<T1, T2> view) => new View<T1, T2>(view).Invoke(this);
    public void View<T1, T2, T3>(ViewFunc<T1, T2, T3> view) => new View<T1, T2, T3>(view).Invoke(this);
    public void View<T1, T2, T3, T4>(ViewFunc<T1, T2, T3, T4> view) => new View<T1, T2, T3, T4>(view).Invoke(this);
    public void View<T1, T2, T3, T4, T5>(ViewFunc<T1, T2, T3, T4, T5> view) => new View<T1, T2, T3, T4, T5>(view).Invoke(this);
    public void View<T1, T2, T3, T4, T5, T6>(ViewFunc<T1, T2, T3, T4, T5, T6> view) => new View<T1, T2, T3, T4, T5, T6>(view).Invoke(this);
    public void View<T1, T2, T3, T4, T5, T6, T7>(ViewFunc<T1, T2, T3, T4, T5, T6, T7> view) => new View<T1, T2, T3, T4, T5, T6, T7>(view).Invoke(this);
    public void View<T1, T2, T3, T4, T5, T6, T7, T8>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8> view) => new View<T1, T2, T3, T4, T5, T6, T7, T8>(view).Invoke(this);
    public void View<T1, T2, T3, T4, T5, T6, T7, T8, T9>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9> view) => new View<T1, T2, T3, T4, T5, T6, T7, T8, T9>(view).Invoke(this);
    public void View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> view) => new View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(view).Invoke(this);
    public void View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> view) => new View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(view).Invoke(this);
    public void View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> view) => new View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(view).Invoke(this);

    public void Update() {
        if (dtLastTime == 0) dtLastTime = dtStopwatch.Elapsed.TotalSeconds;
        double currentTime = dtStopwatch.Elapsed.TotalSeconds;
        deltaTime = currentTime - dtLastTime;
        dtLastTime = currentTime;

        systemManager.UpdateAllStages(this);
        eventManager.Dispatch(this);
    }
}