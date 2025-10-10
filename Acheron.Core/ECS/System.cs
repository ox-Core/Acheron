namespace Acheron.Core.ECS;

public class System {
    internal HashSet<Entity> Entities { get; } = [];

    private readonly Delegate func;
    private readonly Signature signature;

    public System(Delegate func, Signature signature) {
        this.func = func;
        this.signature = signature;
    }

    internal void Update(World world, double dt) {
        foreach (var entity in (IEnumerable<Entity>)(Entities.Count > 0 ? Entities : new List<Entity> { default })) {
            switch (func) {
                case Action<World, Entity, double> f: f(world, entity, dt); break;
                case Action<World, Entity> f: f(world, entity); break;
                case Action<World, double> f: f(world, dt); break;
                case Action<World> f: f(world); break;
                default: throw new InvalidOperationException($"Unsupported system signature.");
            }
        }
    }
    
    internal bool Matches(Signature entitySignature) => signature.Count == 0 || signature.IsSubsetOf(entitySignature);
}