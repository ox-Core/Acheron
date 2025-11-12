using System.Drawing;
using System.Security.Cryptography;
using Acheron.Core.ECS;
using Acheron.Engine.Types;

namespace Acheron.Engine.Renderer;

[Component]
public class Material {
    public Shader Shader = Shader.Invalid;
    public Color Color = Color.White;
    public Texture2D Texture = new();

    private string? id = null;

    public string ID {
        get {
            id ??= SHA1.HashData([(byte)Shader.ID, (byte)Texture.Handle, Color.R, Color.G, Color.B, Color.A]).ToString();
            return id!;
        }
    }
}