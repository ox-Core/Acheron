namespace Basic;

using Acheron.Core.ECS;


[Component]
struct Player;

[Component]
struct Health(float v) {
    public float value = v;
};

[Component]
struct ShouldQuit {
    public bool value = false;

    public ShouldQuit() { }
}

class Program {
    [System<Player, Health>()]
    public static void SubtractHealthSystem(World world, ref Player player, ref Health health) {
        health.value -= 1;
        Console.WriteLine($"Health: {health.value}");
        if (health.value <= 0) {
            world.GetSingleton<ShouldQuit>().value = true;
        }
    }

    static void Main(string[] args) {
        var world = new World();

        world.SetSingleton<ShouldQuit>(new());

        world.SpawnWith(new Player(), new Health(20));

        ref var shouldQuit = ref world.GetSingleton<ShouldQuit>();

        while (!shouldQuit.value) {
            world.Update();
        }
    }
}
