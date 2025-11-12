namespace Rainbow;

using System.Drawing;
using Acheron.Core.ECS;
using Acheron.Engine.Renderer;
using Acheron.Engine.Window;

[Component]
record struct ColorCounter(float Value = 0);

class Program {
    [System<ColorCounter>]
    public static void RainbowClearSystem(World world, Entity _, ref ColorCounter colorCounter) {
        colorCounter.Value += (float)world.DeltaTime;

        var t = colorCounter.Value;

        var r = (int)((Math.Sin(t * 0.8) * 0.5 + 0.5) * 255.0);
        var g = (int)((Math.Sin(t * 0.6f + 2.0f) * 0.5f + 0.5f) * 255.0);
        var b = (int)((Math.Sin(t * 0.4 + 4.0f) * 0.5f + 0.5f) * 255.0);

        world.SetSingleton<ClearColor>(new(Color.FromArgb(255, r, g, b)));
    }

    static void Main(string[] args) {
        var world = new World();

        world.SetSingleton(new WindowConfig() {
            Width = 1280, Height = 720, Resizeable = false,
        });

        world.ImportModule<WindowModule>();
        world.ImportModule<RendererModule>();

        ref var window = ref world.GetSingleton<Window>();

        world.SpawnWith<ColorCounter>(new());

        while (!window.ShouldClose) {
            world.Update();
        }
    }
}
