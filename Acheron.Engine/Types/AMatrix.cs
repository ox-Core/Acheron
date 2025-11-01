using System.Numerics;
using Acheron.Core.ECS;

namespace Acheron.Engine.Types;

[Component]
public class AMatrix4 {
    public float[] M = new float[16];

    public float this[int i] {
        get => M[i];
        set => M[i] = value;
    }

    public static AMatrix4 operator *(AMatrix4 left, AMatrix4 right) {
        return Multiply(left, right);
    }

    public static AMatrix4 Identity() {
        var m = new AMatrix4();
        m[0] = m[5] = m[10] = m[15] = 1;
        return m;
    }


    public static AMatrix4 Transform2D(Vector2 translation, Vector2 scale, float rz) {
        var m = Identity();

        m[0] = scale.X;
        m[5] = scale.Y;
        var rm = RotateZ(rz * (MathF.PI / 180f));
        m *= rm;

        m[12] = translation.X;
        m[13] = translation.Y;

        return m;
    }

    public static AMatrix4 Translate(float x, float y, float z) {
        var m = new AMatrix4();
        m[12] = x;
        m[13] = y;
        m[14] = z;
        return m;
    }

    public static AMatrix4 Scale(float sx, float sy, float sz) {
        var m = Identity();
        m[0] = sx;
        m[5] = sy;
        m[10] = sz;
        return m;
    }

    public static AMatrix4 RotateZ(float radians) {
        float c = MathF.Cos(radians);
        float s = MathF.Sin(radians);
        var m = Identity();
        m[0] = c; m[1] = s;
        m[4] = -s; m[5] = c;
        return m;
    }

    public static AMatrix4 Multiply(AMatrix4 a, AMatrix4 b) {
        var result = Identity();

        for (int row = 0; row < 4; row++) {
            for (int col = 0; col < 4; col++) {
                result[col + row * 4] =
                    a[0 + row * 4] * b[col + 0 * 4] +
                    a[1 + row * 4] * b[col + 1 * 4] +
                    a[2 + row * 4] * b[col + 2 * 4] +
                    a[3 + row * 4] * b[col + 3 * 4];
            }
        }

        return result;
    }

    public static AMatrix4 Ortho(float width, float height) {
        var m = Identity();
        m[0] = 2.0f / width;
        m[5] = 2.0f / height;
        m[10] = -1.0f;
        m[12] = -1.0f;
        m[13] = -1.0f;
        m[15] = 1.0f;
        return m;
    }

    public static AMatrix4 OrthoTopLeft(float width, float height) {
        var m = Identity();
        m[0] = 2.0f / width;
        m[5] = -2.0f / height;
        m[10] = -1.0f;
        m[12] = -1.0f;
        m[13] = 1.0f;
        m[15] = 1.0f;
        return m;
    }
}