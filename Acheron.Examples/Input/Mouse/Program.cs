namespace Mouse;

using System.Drawing;
using Acheron.Core.ECS;
using Acheron.Engine.Input;
using Acheron.Engine.Renderer;
using Acheron.Engine.Types;
using Acheron.Engine.Window;
using Silk.NET.GLFW;

[Component]
record struct Cursor();

class Program {
    [Subscribe<MouseMoveEvent>]
    static void MouseMoved(World world, MouseMoveEvent ev) {
        world.View((World world, Entity e, ref Cursor _, ref Transform2D transform, ref BatchedQuad quad) => {
            transform.Position.X = ev.X - quad.Width / 2f;
            transform.Position.Y = ev.Y - quad.Height / 2f;
        });
    }

    [Subscribe<MouseButtonPressedEvent>]
    static void MousePressed(World world, MouseButtonPressedEvent ev) {
        world.View((World world, Entity e, ref Cursor _, ref BatchedQuad quad) => {
            switch(ev.Button) {
                case MouseButton.Left: quad.Color = Color.Red; break;
                case MouseButton.Right: quad.Color = Color.Blue; break;
            }
        });
    }

    [Subscribe<MouseButtonReleasedEvent>]
    static void MouseReleased(World world, MouseButtonReleasedEvent ev) {
        world.View((World world, Entity e, ref Cursor _, ref BatchedQuad quad) => {
            quad.Color = Color.White;
        });
    }

    static void Main(string[] args) {
        var world = new World();

        world.ImportModule<WindowModule>();
        world.ImportModule<RendererModule>();
        world.ImportModule<InputModule>();

        world.SpawnWith(new BatchedQuad() {
            Width = 100,
            Height = 100,
            Color = Color.White,
        }, new Transform2D(), new Cursor());

        ref var win = ref world.GetSingleton<Window>();

        while (!win.ShouldClose) {
            world.Update();
        }
    }
}
