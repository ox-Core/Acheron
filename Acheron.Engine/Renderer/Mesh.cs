using Acheron.Core.ECS;

namespace Acheron.Engine.Renderer;

public class Mesh {
    public uint vao;
    public uint vbo;
    public uint ebo;

    public uint vertCount;
}

[Component]
public class Mesh2D : Mesh { };

[Component]
public class Mesh3D : Mesh { };