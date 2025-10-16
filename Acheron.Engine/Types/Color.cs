using Acheron.Core.ECS;

namespace Acheron.Engine.Types;

[Component]
public class Color {
    public byte r = 0, g = 0, b = 0, a = 255;

    public Color() { }
    public Color(byte r, byte g, byte b, byte a) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    public Color(uint hex) {
        r = (byte)((hex & 0xff000000) >> 24);
        g = (byte)((hex & 0x00ff0000) >> 16);
        b = (byte)((hex & 0x0000ff00) >> 8);
        a = (byte)((hex & 0x000000ff) >> 0);
    }

    public float[] ToFloat() {
        return [r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f];
    }
}