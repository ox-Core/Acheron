namespace Basic;

using Acheron.Core.ECS;
using Acheron.Engine.Renderer;
using Acheron.Engine.Window;

class Program {

    static void Main(string[] args) {
        var world = new World();

        world.ImportModule<WindowModule>();
        world.ImportModule<RendererModule>();

        ref var window = ref world.GetSingleton<Window>();

        while (!window.shouldClose) {
            world.Update();
        }
    }
}
