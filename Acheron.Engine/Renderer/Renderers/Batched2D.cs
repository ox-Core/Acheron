using System.Text.RegularExpressions;
using Acheron.Core.ECS;
using Acheron.Engine.Types;
using Silk.NET.Core;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Renderer.Renderers;

class Batch2D(Material material) {
    public BatchedQuadVertex[] Vertices = new BatchedQuadVertex[1024];
    public uint[] Indices = new uint[1024];

    public int VertexCount = 0;
    public int IndexCount = 0;

    public Material Material = material;

    public void AddQuad(BatchedQuadVertex[] verts, uint[] indices) {
        int vertOffset = VertexCount;
        int newVertCount = VertexCount + verts.Length;
        int newIndCount = IndexCount + indices.Length;

        if (newVertCount > Vertices.Length)
            Array.Resize(ref Vertices, Math.Max(newVertCount, Vertices.Length * 2));
        if (newIndCount > Indices.Length)
            Array.Resize(ref Indices, Math.Max(newIndCount, Indices.Length * 2));

        verts.CopyTo(Vertices.AsSpan(VertexCount));

        var dst = Indices.AsSpan(IndexCount);
        for (int i = 0; i < indices.Length; i++)
            dst[i] = indices[i] + (uint)vertOffset;

        VertexCount = newVertCount;
        IndexCount = newIndCount;
    }
}

class BatchRenderer2D {
    public uint VAO;
    public uint VBO;
    public uint EBO;
    public Dictionary<string, Batch2D> Batches = [];

    public unsafe BatchRenderer2D(World world) {
        var gl = world.GetSingleton<GLApi>().GL();

        VAO = gl.GenVertexArray();
        gl.BindVertexArray(VAO);

        VBO = gl.GenBuffer();
        EBO = gl.GenBuffer();

        gl.BindBuffer(GLEnum.ArrayBuffer, VBO);
        gl.BindBuffer(GLEnum.ElementArrayBuffer, EBO);

        gl.VertexAttribPointer(0, 3, GLEnum.Float, false, (uint)sizeof(BatchedQuadVertex), (void*)0);
        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(1, 2, GLEnum.Float, false, (uint)sizeof(BatchedQuadVertex), (void*)(3 * sizeof(float)));
        gl.EnableVertexAttribArray(1);
        gl.VertexAttribPointer(2, 4, GLEnum.Float, false, (uint)sizeof(BatchedQuadVertex), (void*)(5 * sizeof(float)));
        gl.EnableVertexAttribArray(2);
    }

    [System("Render2D")]
    static unsafe void BatchRenderer2DDraw(World world) {
        var window = world.GetSingleton<Window.Window>();
        var gl = world.GetSingleton<GLApi>().GL();
        var batchRenderer = world.GetSingleton<RendererInternal>().BatchRenderer!;

        gl.BindVertexArray(batchRenderer.VAO);

        gl.BindBuffer(GLEnum.ArrayBuffer, batchRenderer.VBO);

        var vertexCount = 0;
        var indexCount = 0;
        foreach (var batch in batchRenderer.Batches) {
            vertexCount += batch.Value.VertexCount;
            indexCount += batch.Value.IndexCount;
        }

        foreach (var batch in batchRenderer.Batches) {
            gl.BindBuffer(GLEnum.ArrayBuffer, batchRenderer.VBO);
            gl.BufferData<BatchedQuadVertex>(GLEnum.ArrayBuffer, batch.Value.Vertices, GLEnum.DynamicDraw);

            gl.BindBuffer(GLEnum.ElementArrayBuffer, batchRenderer.EBO);
            gl.BufferData<uint>(GLEnum.ElementArrayBuffer, batch.Value.Indices, GLEnum.DynamicDraw);

            batch.Value.Material.Shader.Bind();
            if (batch.Value.Material.Texture.Loaded && batch.Value.Material.Texture.Handle != 0) {
                gl.ActiveTexture(GLEnum.Texture0);
                gl.BindTexture(GLEnum.Texture2D, batch.Value.Material.Texture.Handle);
                batch.Value.Material.Shader.SetUniform("u_Texture", 0);
                batch.Value.Material.Shader.SetUniform("u_UseTexture", true);
            } else {
                batch.Value.Material.Shader.SetUniform("u_UseTexture", false);
            }

            batch.Value.Material.Shader.SetUniform("u_ViewProj", AMatrix4.OrthoTopLeft(window.Size.X, window.Size.Y));

            gl.DrawElements(GLEnum.Triangles, (uint)batch.Value.IndexCount, GLEnum.UnsignedInt, (void*)0);
        }
    }


    [System("PostRender")]
    static void CleanBatchRenderer(World world) {
        world.GetSingleton<RendererInternal>().BatchRenderer!.Batches.Clear();
    }
}