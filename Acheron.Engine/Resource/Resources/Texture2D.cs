using Acheron.Core.ECS;
using Acheron.Engine.Renderer;
using Acheron.Engine.Types;
using Silk.NET.OpenGL;
using StbImageSharp;

namespace Acheron.Engine.Resource.Resources;

public class Texture2DResourceManager {
    public Dictionary<string, Texture2D> Cache = [];

    public unsafe Texture2D Load(World world, string path) {
        if (Cache.TryGetValue(path, out Texture2D? value))
            return value;

        ImageResult imgResult;

        try {
            var imgStream = File.OpenRead(path);
            imgResult = ImageResult.FromStream(imgStream, ColorComponents.RedGreenBlueAlpha);
        } catch (Exception e) {
            Console.WriteLine($"[Acheron] Failed to load texture '{path}': {e}");
            return new Texture2D();
        }

        Console.WriteLine($"[Acheron] Loaded texture '{path}' at size {imgResult.Width}x{imgResult.Height}");

        var gl = world.GetSingleton<GLApi>().GL();

        var handle = gl.GenTexture();
        Console.WriteLine($"[Acheron] Generated OpenGL texture {handle} from '{path}'");
        gl.BindTexture(GLEnum.Texture2D, handle);

        fixed (void* data = &imgResult.Data[0]) {
            gl.TexImage2D(GLEnum.Texture2D, 0, (int)GLEnum.Rgba8, (uint)imgResult.Width, (uint)imgResult.Height, 0, GLEnum.Rgba, GLEnum.UnsignedByte, data);
        }

        var linear = (int)GLEnum.Linear;
        var repeat = (int)GLEnum.Repeat;

        gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMinFilter, in linear);
        gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureMagFilter, in linear);
        
        gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapS, in repeat);
        gl.TexParameterI(GLEnum.Texture2D, GLEnum.TextureWrapT, in repeat);

        var tex = new Texture2D() {
            Handle = handle,
            Width = imgResult.Width,
            Height = imgResult.Height,
            Loaded = true,
        };

        Cache[path] = tex;

        return tex;
    }
}