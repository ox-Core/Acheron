namespace Acheron.Core.ECS;

public interface IViewInvoker {
    void Invoke(World world);
}

public delegate void ViewFunc<T1>(World world, Entity entity, ref T1 c1);
public class View<T1>(ViewFunc<T1> func) : IViewInvoker {
    private readonly ViewFunc<T1> func = func;

    public void Invoke(World world) {
        foreach (var e in world.Entities) {
            if (!world.HasComponent<T1>(e)) continue;
            ref var c1 = ref world.GetComponent<T1>(e);
            func(world, e, ref c1);
        }
    }
}

public delegate void ViewFunc<T1, T2>(World world, Entity entity, ref T1 c1, ref T2 c2);
public class View<T1, T2>(ViewFunc<T1, T2> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e)) continue;
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            func(world, e, ref c1, ref c2);
        }
    }
}

public delegate void ViewFunc<T1, T2, T3>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3);
public class View<T1, T2, T3>(ViewFunc<T1, T2, T3> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e)) continue;
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            func(world, e, ref c1, ref c2, ref c3);
        }
    }
}

public delegate void ViewFunc<T1, T2, T3, T4>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4);
public class View<T1, T2, T3, T4>(ViewFunc<T1, T2, T3, T4> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e)) continue;
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4);
        }
    }
}

public delegate void ViewFunc<T1, T2, T3, T4, T5>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5);
public class View<T1, T2, T3, T4, T5>(ViewFunc<T1, T2, T3, T4, T5> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4, T5> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e) ||
                !world.HasComponent<T5>(e)) continue;
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5);
        }
    }
}

public delegate void ViewFunc<T1, T2, T3, T4, T5, T6>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6);
public class View<T1, T2, T3, T4, T5, T6>(ViewFunc<T1, T2, T3, T4, T5, T6> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4, T5, T6> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e) ||
                !world.HasComponent<T5>(e) ||
                !world.HasComponent<T6>(e)) continue;
            ref var c1 = ref world.GetComponent<T1>(e);
            ref var c2 = ref world.GetComponent<T2>(e);
            ref var c3 = ref world.GetComponent<T3>(e);
            ref var c4 = ref world.GetComponent<T4>(e);
            ref var c5 = ref world.GetComponent<T5>(e);
            ref var c6 = ref world.GetComponent<T6>(e);
            func(world, e, ref c1, ref c2, ref c3, ref c4, ref c5, ref c6);
        }
    }
}

public delegate void ViewFunc<T1, T2, T3, T4, T5, T6, T7>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7);
public class View<T1, T2, T3, T4, T5, T6, T7>(ViewFunc<T1, T2, T3, T4, T5, T6, T7> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4, T5, T6, T7> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e) ||
                !world.HasComponent<T5>(e) ||
                !world.HasComponent<T6>(e) ||
                !world.HasComponent<T7>(e)) continue;
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
}

public delegate void ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8);
public class View<T1, T2, T3, T4, T5, T6, T7, T8>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e) ||
                !world.HasComponent<T5>(e) ||
                !world.HasComponent<T6>(e) ||
                !world.HasComponent<T7>(e) ||
                !world.HasComponent<T8>(e)) continue;
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
}

public delegate void ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8, ref T9 c9);
public class View<T1, T2, T3, T4, T5, T6, T7, T8, T9>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e) ||
                !world.HasComponent<T5>(e) ||
                !world.HasComponent<T6>(e) ||
                !world.HasComponent<T7>(e) ||
                !world.HasComponent<T8>(e) ||
                !world.HasComponent<T9>(e)) continue;
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
}

public delegate void ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8, ref T9 c9, ref T10 c10);
public class View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e) ||
                !world.HasComponent<T5>(e) ||
                !world.HasComponent<T6>(e) ||
                !world.HasComponent<T7>(e) ||
                !world.HasComponent<T8>(e) ||
                !world.HasComponent<T9>(e) ||
                !world.HasComponent<T10>(e)) continue;
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
}

public delegate void ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8, ref T9 c9, ref T10 c10, ref T11 c11);
public class View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e) ||
                !world.HasComponent<T5>(e) ||
                !world.HasComponent<T6>(e) ||
                !world.HasComponent<T7>(e) ||
                !world.HasComponent<T8>(e) ||
                !world.HasComponent<T9>(e) ||
                !world.HasComponent<T10>(e) ||
                !world.HasComponent<T11>(e)) continue;
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
}

public delegate void ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(World world, Entity entity, ref T1 c1, ref T2 c2, ref T3 c3, ref T4 c4, ref T5 c5, ref T6 c6, ref T7 c7, ref T8 c8, ref T9 c9, ref T10 c10, ref T11 c11, ref T12 c12);
public class View<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> func) : IViewInvoker {
    private readonly ViewFunc<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> func = func;

    public void Invoke(World world) {
        foreach(var e in world.Entities) {
            if (!world.HasComponent<T1>(e) ||
                !world.HasComponent<T2>(e) ||
                !world.HasComponent<T3>(e) ||
                !world.HasComponent<T4>(e) ||
                !world.HasComponent<T5>(e) ||
                !world.HasComponent<T6>(e) ||
                !world.HasComponent<T7>(e) ||
                !world.HasComponent<T8>(e) ||
                !world.HasComponent<T9>(e) ||
                !world.HasComponent<T10>(e) ||
                !world.HasComponent<T11>(e) ||
                !world.HasComponent<T12>(e)) continue;
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
}