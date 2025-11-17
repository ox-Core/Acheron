namespace Acheron.Core.ECS;

public abstract class Module {
    public static readonly HashSet<Type> LoadedModules = new();

    public abstract void Register(World world);
    public void RegisterWithDeps(World world) {
        if(LoadedModules.Contains(GetType()))
            return;

        foreach(var t in Dependencies) {
            if(!LoadedModules.Contains(t)) {
                var dep = (Module)Activator.CreateInstance(t)!;
                dep.RegisterWithDeps(world);
            }
        }

        Register(world);
        LoadedModules.Add(GetType());
    }

    public virtual Type[] Dependencies { get; } = [];
}
