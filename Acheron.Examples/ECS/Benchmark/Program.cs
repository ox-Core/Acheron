namespace Benchmark;

using Acheron.Core.ECS;

[Component]
public struct Component1 {
    public int Value;
}
[Component]
public struct Component2 {
    public int Value;
}
[Component]
public struct Component3 {
    public int Value;
}


class Program {

    [Component]
    private record Padding1();
    [Component]
    private record Padding2();
    [Component]
    private record Padding3();
    [Component]
    private record Padding4();

    [System<Component1, Component2>]
    static void RunSystem(World world, Entity e, ref Component1 c1, ref Component2 c2) {
        c1.Value += c2.Value;
    }

    static void Main(string[] args) {
        var world = new World();

        for (int i = 0; i < 100000; i++) {
            Entity e = (i % 4) switch {
                0 => world.SpawnWith<Component1, Component2, Padding1>(default, new() { Value = 1 }, default!),
                1 => world.SpawnWith<Component1, Component2, Padding2>(default, new() { Value = 1 }, default!),
                2 => world.SpawnWith<Component1, Component2, Padding3>(default, new() { Value = 1 }, default!),
                _ => world.SpawnWith<Component1, Component2, Padding4>(default, new() { Value = 1 }, default!),
            };
        }

        world.Update();
        world.Update();

        Console.WriteLine(world.DeltaTime * 1000f);
    }
}
