using System.Numerics;

namespace Acheron.Engine.Types;

class Transform2D {
    public Vector2 Position = Vector2.Zero;
    public float Rotation = 0;
    public Vector2 Scale = Vector2.One;

    Matrix4x4 ToMatrix4() {
        return Matrix4x4.Identity *
                Matrix4x4.CreateTranslation(Position.X, Position.Y, 0) *
                Matrix4x4.CreateRotationZ(Rotation * (MathF.PI / 180)) *
                Matrix4x4.CreateScale(Scale.X, Scale.Y, 1);
    }
}
