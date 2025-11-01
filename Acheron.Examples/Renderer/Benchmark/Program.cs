namespace Benchmark;

using System.Drawing;
using Acheron.Core.ECS;
using Acheron.Engine.Renderer;
using Acheron.Engine.Window;

[Component]
record struct ColorCounter(float Value = 0);

class Program {

    static void Main(string[] args) {
        var world = new World();

        world.ImportModule<WindowModule>();
        world.ImportModule<RendererModule>();

        ref var window = ref world.GetSingleton<Window>();

        world.SpawnWith<ColorCounter>(new());

        while (!window.shouldClose) {
            world.Update();
        }
    }
}
