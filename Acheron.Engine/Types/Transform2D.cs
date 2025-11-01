using System.Numerics;
using Acheron.Core.ECS;

namespace Acheron.Engine.Types;

[Component]
public class Transform2D {
    public Vector2 Position = Vector2.Zero;
    public float Rotation = 0;
    public Vector2 Scale = Vector2.One;

    public AMatrix4 ToMatrix4() {
        return AMatrix4.Transform2D(Position, Scale, Rotation);
    }
}
