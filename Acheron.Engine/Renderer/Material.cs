using System.Drawing;
using Acheron.Core.ECS;

namespace Acheron.Engine.Renderer;

[Component]
public class Material {
    public Shader Shader = Shader.Invalid;
    public Color Color = Color.White;
}