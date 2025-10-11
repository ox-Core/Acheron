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
    [System(typeof(Player), typeof(Health))]
    static void SubtractHealthSystem(World world, Entity e) {
        ref var health = ref world.GetComponent<Health>(e);
        health.value -= 1;
        Console.WriteLine($"Health: {health.value}");
        if (health.value <= 0) {
            world.GetSingleton<ShouldQuit>().value = true;
        }
    }

    static void Main(string[] args) {
        var world = new World();

        world.SetSingleton<ShouldQuit>(new ShouldQuit());

        world.SpawnWith(new Player(), new Health(20));

        ref var shouldQuit = ref world.GetSingleton<ShouldQuit>();

        while (!shouldQuit.value) {
            world.Update();
        }
    }
}
