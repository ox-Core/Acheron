using System.Drawing;
using System.Numerics;
using Acheron.Core.ECS;

namespace Acheron.Engine.Types;

[Component]
public class ColorWrapper {
    public static Color From(byte r, byte g, byte b, byte a) {
        return Color.FromArgb(a, r, g, b);
    }

    public static Color From(uint hex) {
        var r = (byte)((hex & 0xff000000) >> 24);
        var g = (byte)((hex & 0x00ff0000) >> 16);
        var b = (byte)((hex & 0x0000ff00) >> 8);
        var a = (byte)((hex & 0x000000ff) >> 0);

        return Color.FromArgb(a, r, g, b);
    }

    public static float[] ToFloat(Color color) {
        return [color.R / 255.0f, color.G / 255.0f, color.B / 255.0f, color.A / 255.0f];
    }

    public static Vector4 ToVec4(Color color) {
        var colorArr = ToFloat(color);
        return new Vector4(colorArr[0], colorArr[1], colorArr[2], colorArr[3]);
    } 
}