namespace Benchmark;

using System.Drawing;
using System.Numerics;
using System.Security.Cryptography;
using Acheron.Core.ECS;
using Acheron.Engine.Renderer;
using Acheron.Engine.Resource;
using Acheron.Engine.Types;
using Acheron.Engine.Window;

[Component]
record struct Velocity(float X, float Y);

class Program {
    static int GetRandom(int limit) {
        return RandomNumberGenerator.GetInt32(limit);
    }

    static Color RandomColor() {
        return Color.FromArgb(255, GetRandom(255), GetRandom(255), GetRandom(255));
    }


    [System<RenderableQuad, Transform2D, Velocity>]
    static void MoveQuads(World world, Entity e, ref RenderableQuad quad, ref Transform2D transform, ref Velocity velocity) {
        ref var window = ref world.GetSingleton<Window>();
        transform.Position.X += velocity.X * (float)world.DeltaTime;
        transform.Position.Y += velocity.Y * (float)world.DeltaTime;
        if (transform.Position.X < 0) {
            transform.Position.X = 0;
            velocity.X *= -1;
            quad.Color = RandomColor();
        } else if (transform.Position.X + quad.Width > window.Size.X) {
            transform.Position.X = window.Size.X - quad.Width;
            velocity.X *= -1;
            quad.Color = RandomColor();
        }
        // Console.WriteLine(velocity.Y);
        if(transform.Position.Y < 0) {
            transform.Position.Y = 0;
            velocity.Y *= -1;
            quad.Color = RandomColor();
        } else if(transform.Position.Y + quad.Height > window.Size.Y) {
            transform.Position.Y = window.Size.Y - quad.Height;
            velocity.Y *= -1;
            quad.Color = RandomColor();
        }
    }

    static void SpawnRandom(World world) {
        const float SPEED = 150f;
        var angle = (float)((float)GetRandom(int.MaxValue) / int.MaxValue * 2f * 3.14159265f);

        var velocity = new Velocity(MathF.Cos(angle) * SPEED, MathF.Sin(angle) * SPEED);

        world.SpawnWith(new RenderableQuad() {
            Width = 100,
            Height = 100,
            Color = Color.FromArgb(255, GetRandom(255), GetRandom(255), GetRandom(255)),
        }, new Transform2D() {
            Position = new Vector2(RandomNumberGenerator.GetInt32(500), RandomNumberGenerator.GetInt32(500)),
        }, new Material() {
            Texture = ResourceManager.Get(world).LoadTexture("Assets/cool.png")
        }, velocity);
    }

    static void Main(string[] args) {
        var world = new World();

        world.ImportModule<WindowModule>();
        world.ImportModule<RendererModule>();
        world.ImportModule<ResourceModule>();

        ref var window = ref world.GetSingleton<Window>();

        for (int i = 0; i < 10000; i++) {
            SpawnRandom(world);
        }

        var timer = world.DeltaTime;
        var avg = new float[100];
        var avgi = 0;
        while (!window.ShouldClose) {
            avg[avgi++ % avg.Length] += 1f / (float)world.DeltaTime;

            if (timer >= 0.5) {
                Console.WriteLine(avg.Average());
                Array.Clear(avg, 0, avg.Length);
                timer = 0;
                avgi = 0;
            }
            timer += world.DeltaTime;
            world.Update();
        }
    }
}
