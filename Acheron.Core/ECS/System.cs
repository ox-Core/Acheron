using System.Reflection;

namespace Acheron.Core.ECS;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public class SystemAttribute(string stage = "Update") : Attribute { public string Stage { get; } = stage; }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4, T5>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4, T5, T6>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4, T5, T6, T7>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4, T5, T6, T7, T8>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(string stage = "Update") : SystemAttribute(stage) { }

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
public sealed class SystemAttribute<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(string stage = "Update") : SystemAttribute(stage) { }

public interface ISystem { void Update(World world); bool Matches(Signature signature); HashSet<Entity> Entities { get; } }

public delegate void SystemFunc(World world);

public class System : ISystem {
    private readonly SystemFunc func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = new();

    public System(SystemFunc func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) => func(world);

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1>(World world, Entity entity, ref T1 c1);
public class System<T1> : ISystem {
    private readonly SystemFunc<T1> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            func(world, e, ref c1);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2>(World world, Entity entity, ref T1 c1, ref T2 c2);
public class System<T1, T2> : ISystem {
    private readonly SystemFunc<T1, T2> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            func(world, e, ref c1, ref c2);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3);
public class System<T1, T2, T3> : ISystem {
    private readonly SystemFunc<T1, T2, T3> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            func(world, e, ref c1, ref c2, ref c3);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4);
public class System<T1, T2, T3, T4> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4, T5>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5);
public class System<T1, T2, T3, T4, T5> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4, T5> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4, T5> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4, T5, T6>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6);
public class System<T1, T2, T3, T4, T5, T6> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4, T5, T6> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4, T5, T6> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            ref var c6 = ref world.GetComponent<T6>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4, T5, T6, T7>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7);
public class System<T1, T2, T3, T4, T5, T6, T7> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4, T5, T6, T7> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4, T5, T6, T7> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            ref var c6 = ref world.GetComponent<T6>(e);
            ref var c7 = ref world.GetComponent<T7>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8);
public class System<T1, T2, T3, T4, T5, T6, T7, T8> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            ref var c6 = ref world.GetComponent<T6>(e);
            ref var c7 = ref world.GetComponent<T7>(e);
            ref var c8 = ref world.GetComponent<T8>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8, ref T9 c9);
public class System<T1, T2, T3, T4, T5, T6, T7, T8, T9> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            ref var c6 = ref world.GetComponent<T6>(e);
            ref var c7 = ref world.GetComponent<T7>(e);
            ref var c8 = ref world.GetComponent<T8>(e);
            ref var c9 = ref world.GetComponent<T9>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8, ref c9);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8, ref T9 c9, ref T10 c10);
public class System<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            ref var c6 = ref world.GetComponent<T6>(e);
            ref var c7 = ref world.GetComponent<T7>(e);
            ref var c8 = ref world.GetComponent<T8>(e);
            ref var c9 = ref world.GetComponent<T9>(e);
            ref var c10 = ref world.GetComponent<T10>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8, ref c9, ref c10);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8, ref T9 c9, ref T10 c10, ref T11 c11);
public class System<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            ref var c6 = ref world.GetComponent<T6>(e);
            ref var c7 = ref world.GetComponent<T7>(e);
            ref var c8 = ref world.GetComponent<T8>(e);
            ref var c9 = ref world.GetComponent<T9>(e);
            ref var c10 = ref world.GetComponent<T10>(e);
            ref var c11 = ref world.GetComponent<T11>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8, ref c9, ref c10, ref c11);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

public delegate void SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8, ref T9 c9, ref T10 c10, ref T11 c11, ref T12 c12);
public class System<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : ISystem {
    private readonly SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> func;
    private readonly Signature signature;
    public HashSet<Entity> Entities { get; } = [];

    public System(SystemFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    public void Update(World world) {
        foreach (var e in Entities) {
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            ref var c6 = ref world.GetComponent<T6>(e);
            ref var c7 = ref world.GetComponent<T7>(e);
            ref var c8 = ref world.GetComponent<T8>(e);
            ref var c9 = ref world.GetComponent<T9>(e);
            ref var c10 = ref world.GetComponent<T10>(e);
            ref var c11 = ref world.GetComponent<T11>(e);
            ref var c12 = ref world.GetComponent<T12>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6, ref c7, ref c8, ref c9, ref c10, ref c11, ref c12);
        }
    }

    public bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}

static class SystemHelper {
    public static ISystem GetSystemType(Type[] componentTypes, Signature signature, MethodInfo method) {
        if (!method.IsStatic) {
            throw new InvalidOperationException($"System methods must be static. Method '{method.DeclaringType!.FullName}.{method.Name}' is not static.");
        }
        return componentTypes.Length switch {
            0 =>  (ISystem)Activator.CreateInstance(
                    typeof(System),
                    Delegate.CreateDelegate(typeof(SystemFunc), method),
                    signature)!,
            1 => (ISystem)Activator.CreateInstance(
                    typeof(System<>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<>).MakeGenericType(componentTypes), method),
                    signature)!,
            2 => (ISystem)Activator.CreateInstance(
                    typeof(System<,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,>).MakeGenericType(componentTypes), method),
                    signature)!,
            3 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            4 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            5 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            6 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            7 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            8 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,,,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,,,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            9 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,,,,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,,,,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            10 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,,,,,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,,,,,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            11 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,,,,,,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,,,,,,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            12 => (ISystem)Activator.CreateInstance(
                    typeof(System<,,,,,,,,,,,>).MakeGenericType(componentTypes),
                    Delegate.CreateDelegate(typeof(SystemFunc<,,,,,,,,,,,>).MakeGenericType(componentTypes), method),
                    signature)!,
            _ => throw new NotSupportedException($"Systems with {componentTypes.Length} components are not supported")
        };
    } 
}
