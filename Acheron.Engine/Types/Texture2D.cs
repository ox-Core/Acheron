using Acheron.Core.ECS;

namespace Acheron.Engine.Types;

[Component]
public class Texture2D {
    public uint Handle;

    public int Width = 0, Height = 0;
    public bool Loaded = false;
}