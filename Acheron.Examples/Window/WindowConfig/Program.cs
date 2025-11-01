namespace Basic;

using Acheron.Core.ECS;
using Acheron.Engine.Renderer;
using Acheron.Engine.Window;

class Program {

    static void Main(string[] args) {
        var world = new World();

        world.SetSingleton(new WindowConfig() {
            Title = "Acheron - Window Config Example",
            Width = 1280,
            Height = 720,
            Resizeable = false,
        });

        world.ImportModule<WindowModule>();

        ref var windowHandle = ref world.GetSingleton<Window>();

        while(!windowHandle.shouldClose) {
            world.Update();
        }
    }
}
