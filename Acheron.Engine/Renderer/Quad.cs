using System.Drawing;
using System.Runtime.InteropServices;
using Acheron.Core.ECS;
using Acheron.Engine.Renderer.Renderers;
using Acheron.Engine.Types;

namespace Acheron.Engine.Renderer;

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public readonly struct QuadVertex(float x, float y, float z, float u, float v, Color color) {
    [FieldOffset(0)]
    public readonly float x = x;

    [FieldOffset(4)]
    public readonly float y = y;

    [FieldOffset(8)]
    public readonly float z = z;

    [FieldOffset(12)]
    public readonly float u = u;

    [FieldOffset(16)]
    public readonly float v = v;

    [FieldOffset(20)]
    public readonly float r = color.R / 255f;

    [FieldOffset(24)]
    public readonly float g = color.G / 255f;

    [FieldOffset(28)]
    public readonly float b = color.B / 255f;
    
    [FieldOffset(32)]
    public readonly float a = color.A / 255f;
}


[Component]
public class RenderableQuad {
    public float Width = 0, Height = 0;
    public float Layer = 0;
    public Color Color = Color.White;

    static readonly QuadVertex[] vertices = new QuadVertex[4];
    static uint[] indices = [0, 1, 2, 2, 3, 0]; 

    [System<RenderableQuad>("PreRender")]
    static void SubmitBatchedQuad(World world, Entity e, ref RenderableQuad quad) {
        ref var batchRenderer = ref world.GetSingleton<RendererInternal>().BatchRenderer;

        if (!world.HasComponent<Material>(e)) {
            var mat = new Material();
            var batchShader = world.GetSingleton<BatchShader>();

            mat.Shader = batchShader.Shader;

            world.AddComponent(e, mat);
        }

        if (world.HasComponent<Material>(e)) {
            ref var mat = ref world.GetComponent<Material>(e);
            ref var window = ref world.GetSingleton<Window.Window>();
            var batchShader = world.GetSingleton<BatchShader>();

            mat.Shader = batchShader.Shader;

            var transform = new Transform2D();
            if (world.HasComponent<Transform2D>(e))
                transform = world.GetComponent<Transform2D>(e);
            
            vertices[0] = new QuadVertex(transform.Position.X, transform.Position.Y, quad.Layer, 0f, 0f, quad.Color);
            vertices[1] = new QuadVertex(quad.Width + transform.Position.X, transform.Position.Y, quad.Layer, 1f, 0f, quad.Color);
            vertices[2] = new QuadVertex(quad.Width + transform.Position.X, quad.Height + transform.Position.Y, quad.Layer, 1f, 1f, quad.Color);
            vertices[3] = new QuadVertex(transform.Position.X, quad.Height + transform.Position.Y, quad.Layer, 0f, 1f, quad.Color);

            bool found = false;
            foreach (var batch in batchRenderer!.Batches) {
                if (batch.Key == mat.ID) found = true;
            }

            if(!found) batchRenderer!.Batches[mat.ID] = new Batch2D(mat);

            batchRenderer!.Batches[mat.ID].AddQuad(vertices, indices);
        }
    }
}