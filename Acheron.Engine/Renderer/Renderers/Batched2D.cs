using System.Buffers;
using System.Drawing;
using System.Runtime.CompilerServices;
using Acheron.Core.ECS;
using Acheron.Engine.Types;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Renderer.Renderers;

unsafe class Batch2D : IDisposable {
    public QuadVertex[] Vertices;
    public uint[] Indices;

    bool disposed;

    public int VertexCount;
    public int IndexCount;

    public Material Material;

    public Batch2D(Material material, int startSize = 1024) {
        Material = material;

        Vertices = ArrayPool<QuadVertex>.Shared.Rent(startSize);
        Indices = ArrayPool<uint>.Shared.Rent(startSize);
        
        VertexCount = 0;
        IndexCount = 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int NextSize(int x) {
        if(x <= 0) return 1;
        x--;
        x |= x >> 1;
        x |= x >> 2;
        x |= x >> 4;
        x |= x >> 8;
        x |= x >> 16;
        return x + 1;
    }

    void EnsureCapacity(int newVertCount, int newIndCount) {
        if(newVertCount > Vertices.Length) {
            var newSize = NextSize(newVertCount);
            var newArr = ArrayPool<QuadVertex>.Shared.Rent(newSize);

            fixed(QuadVertex* src = Vertices) {
                fixed(QuadVertex* dst = newArr) {
                    System.Buffer.MemoryCopy(src, dst, (long)newArr.Length * sizeof(QuadVertex), (long)VertexCount * sizeof(QuadVertex));
                }
                ArrayPool<QuadVertex>.Shared.Return(Vertices, clearArray: false);
                Vertices = newArr;
            }
        }

        if(newIndCount > Indices.Length) {
            var newSize = NextSize(newIndCount);
            var newArr = ArrayPool<uint>.Shared.Rent(newSize);

            fixed(uint* src = Indices) {
                fixed(uint* dst = newArr) {
                    System.Buffer.MemoryCopy(src, dst, (long)newArr.Length * sizeof(uint), (long)IndexCount * sizeof(uint));
                }
                ArrayPool<uint>.Shared.Return(Indices, clearArray: false);
                Indices = newArr;
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static void WriteVertex(ref QuadVertex dst, float x, float y, float z, float u, float v, Color color) {
        dst.x = x;
        dst.y = y;
        dst.z = z;
        dst.u = u;
        dst.v = v;
        dst.r = color.R / 255f;
        dst.g = color.G / 255f;
        dst.b = color.B / 255f;
        dst.a = color.A / 255f;
    }

    public void AddQuad(
        float x0, float y0, float z0, float u0, float v0, Color color0,
        float x1, float y1, float z1, float u1, float v1, Color color1,
        float x2, float y2, float z2, float u2, float v2, Color color2,
        float x3, float y3, float z3, float u3, float v3, Color color3
    ) {
        const int quadIndexCount = 6;
        int newVertCount = VertexCount + 4;
        int newIndCount = IndexCount + quadIndexCount;

        EnsureCapacity(newVertCount, newIndCount);

        WriteVertex(ref Vertices[VertexCount + 0], x0, y0, z0, u0, v0, color0);
        WriteVertex(ref Vertices[VertexCount + 1], x1, y1, z1, u1, v1, color1);
        WriteVertex(ref Vertices[VertexCount + 2], x2, y2, z2, u2, v2, color2);
        WriteVertex(ref Vertices[VertexCount + 3], x3, y3, z3, u3, v3, color3);

        uint offs = (uint)VertexCount;
        fixed (uint* pDst = &Indices[IndexCount])
        {
            pDst[0] = offs + 0;
            pDst[1] = offs + 1;
            pDst[2] = offs + 2;
            pDst[3] = offs + 2;
            pDst[4] = offs + 3;
            pDst[5] = offs + 0;
        }

        VertexCount = newVertCount;
        IndexCount = newIndCount;
    }

    public void Clear() {
        VertexCount = 0;
        IndexCount = 0;
    }

    public void Dispose() {
        if(!disposed) {
            ArrayPool<QuadVertex>.Shared.Return(Vertices, clearArray: false);
            ArrayPool<uint>.Shared.Return(Indices, clearArray: false);
            Vertices = null!;
            Indices = null!;
            disposed = true;
        }
    }
}

class BatchRenderer2D {
    public uint VAO;
    public uint VBO;
    public uint EBO;
    public Dictionary<string, Batch2D> Batches = [];

    public void ClearBatches() {
        foreach(var batch in Batches) {
            batch.Value.Clear();
        }
    }

    public unsafe BatchRenderer2D(World world) {
        var gl = world.GetSingleton<GLApi>().GL();

        VAO = gl.GenVertexArray();
        gl.BindVertexArray(VAO);

        VBO = gl.GenBuffer();
        EBO = gl.GenBuffer();

        gl.BindBuffer(GLEnum.ArrayBuffer, VBO);
        gl.BindBuffer(GLEnum.ElementArrayBuffer, EBO);

        gl.VertexAttribPointer(0, 3, GLEnum.Float, false, (uint)sizeof(QuadVertex), (void*)0);
        gl.EnableVertexAttribArray(0);
        gl.VertexAttribPointer(1, 2, GLEnum.Float, false, (uint)sizeof(QuadVertex), (void*)(3 * sizeof(float)));
        gl.EnableVertexAttribArray(1);
        gl.VertexAttribPointer(2, 4, GLEnum.Float, false, (uint)sizeof(QuadVertex), (void*)(5 * sizeof(float)));
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
            gl.BufferData<QuadVertex>(GLEnum.ArrayBuffer, batch.Value.Vertices, GLEnum.DynamicDraw);

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
        world.GetSingleton<RendererInternal>().BatchRenderer!.ClearBatches();
    }
}