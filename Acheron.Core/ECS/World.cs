using System.Reflection;

using Acheron.Core.ECS.Internal;

namespace Acheron.Core.ECS;

public class World {
    private EntityManager entityManager = new();
    private SystemManager systemManager = new();
    private ComponentManager componentManager = new();

    public World() {
        foreach (var type in Assembly.GetCallingAssembly().GetTypes()) {
            if (type.GetCustomAttributes(typeof(ComponentAttribute), true).Length > 0) {
                RegisterComponent(type);
            }
        }
    }

    public Entity Spawn() {
        return entityManager.Spawn();
    }

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

    public void RegisterComponent<T>() {
        componentManager.RegisterComponent<T>();
    }

    public void RegisterComponent(Type t) {
        var methodInfo = typeof(ComponentManager).GetMethod(nameof(ComponentManager.RegisterComponent));
        if (methodInfo == null)
            throw new InvalidOperationException("SOMETHING VERY BAD HAS HAPPENED");

        var method = methodInfo.MakeGenericMethod(t);
        method.Invoke(componentManager, null);
    }

    public bool HasComponent<T>(Entity entity) {
        return componentManager.HasComponent<T>(entity);
    }

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

    public ref T GetComponent<T>(Entity entity) {
        return ref componentManager.GetComponent<T>(entity);
    }

    public System RegisterSystem(Delegate func, IEnumerable<Type> components, string stage = "Update") {
        var signature = new Signature(components.Select(t => componentManager.GetComponentID(t)).ToHashSet());
        return systemManager.Register(func, signature, stage);
    }

    public System RegisterSystem(Delegate func, string stage = "Update") {
        return RegisterSystem(func, [], stage);
    }

    public void SetSingleton<T>(T instance) where T : new() {
        SingletonStorage<T>.Set(instance);
    }

    public ref T GetSingleton<T>() where T : new() {
        return ref SingletonStorage<T>.Get();
    }

    public bool IsSingletonSet<T>() where T : new() {
        return SingletonStorage<T>.IsSet;   
    }

    public void ImportModule<T>() where T : Module, new() {
        new T().Register(this);
    }

    public void Update() {
        systemManager.UpdateAllStages(this, 0);
    }
}