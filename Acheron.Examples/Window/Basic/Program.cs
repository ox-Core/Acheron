namespace Basic;

using Acheron.Core.ECS;
using Acheron.Engine.Window;

class Program {

    static void Main(string[] args) {
        var world = new World();

        world.ImportModule<WindowModule>();

        ref var window = ref world.GetSingleton<Window>();

        while (!window.shouldClose) {
            world.Update();
        }
    }
}
