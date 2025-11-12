namespace Basic;

using Acheron.Core.ECS;
using Acheron.Engine.Renderer;
using Acheron.Engine.Window;

class Program {

    static void Main(string[] args) {
        var world = new World();

        world.ImportModule<WindowModule>();

        ref var windowHandle = ref world.GetSingleton<Window>();

        while(!windowHandle.ShouldClose) {
            world.Update();
        }
    }
}
