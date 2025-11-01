using System.Runtime.InteropServices;
using Acheron.Core.ECS;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Renderer;

[StructLayout(LayoutKind.Explicit, Pack = 1)]
public readonly struct QuadVertex(float x, float y, float z, float u, float v) {
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
}

[Component]
public class RenderableQuad {
    public float Width, Height;
    public float Layer = 0;

    [System<RenderableQuad>("PostUpdate")]
    static unsafe void QuadToMeshSystem(World world, Entity e, ref RenderableQuad quad) {
        if (world.HasComponent<Material>(e)) {
            ref var mat = ref world.GetComponent<Material>(e);
            var basicShader = world.GetSingleton<BasicShader>();

            mat.Shader = basicShader.Shader;
        }

        if (world.HasComponent<Mesh2D>(e)) return;

        var mesh = new Mesh2D {
            vertCount = 6
        };

        QuadVertex[] verts = [
            new QuadVertex(0f, 0f, quad.Layer, 0f, 0f),
            new QuadVertex(quad.Width, 0f, quad.Layer, 1f, 0f),
            new QuadVertex(quad.Width, quad.Height, quad.Layer, 1f, 1f),
            new QuadVertex(0f, quad.Height, quad.Layer, 0f, 1f),
        ];

        uint[] indices = [0, 1, 2, 2, 3, 0];

        var gl = world.GetSingleton<GLApi>().GL();

        mesh.vao = gl.GenVertexArrays(1);
        gl.BindVertexArray(mesh.vao);

        mesh.vbo = gl.GenBuffers(1);
        gl.BindBuffer(GLEnum.ArrayBuffer, mesh.vbo);
        gl.BufferData<QuadVertex>(GLEnum.ArrayBuffer, verts, GLEnum.StaticDraw);

        mesh.ebo = gl.GenBuffers(1);
        gl.BindBuffer(GLEnum.ElementArrayBuffer, mesh.ebo);
        gl.BufferData<uint>(GLEnum.ElementArrayBuffer, indices, GLEnum.StaticDraw);

        gl.VertexAttribPointer(0, 3, GLEnum.Float, false, 5 * sizeof(float), (void*)0);
        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(1, 2, GLEnum.Float, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));
        gl.EnableVertexAttribArray(1);

        world.AddComponent<Mesh2D>(e, mesh);
    }
}