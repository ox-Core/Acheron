namespace Benchmark;

using Acheron.Core.ECS;
using Acheron.Engine.Renderer;
using Acheron.Engine.Resource;
using Acheron.Engine.Types;
using Acheron.Engine.Window;

class Program {

    static void Main(string[] args) {
        var world = new World();

        world.ImportModule<ResourceModule>();
        world.ImportModule<WindowModule>();
        world.ImportModule<RendererModule>();

        ref var window = ref world.GetSingleton<Window>();
        ref var resourceManager = ref world.GetSingleton<ResourceManager>();

        world.SpawnWith(new RenderableQuad() {
            Width = 200, Height = 200, Layer = 0,
        }, new Transform2D() {
            Position = new(100, 100),
        }, new Material() {
            Texture = resourceManager.LoadTexture("Assets/image-1.jpg"),
        });

        while (!window.ShouldClose) {
            world.Update();
        }
    }
}
