using System.Drawing;
using System.Numerics;
using Acheron.Core.ECS;
using Acheron.Engine.Window;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Renderer;

public class GLApi {
    public GL? api;

    public GL Api() {
        return api!;
    }
}

public class RendererModule : Module {
    public static unsafe void SetupRenderer(World world) {
        var glfwApi = world.GetSingleton<GLFWApi>();
        var glfw = glfwApi.Glfw();

        var window = world.GetSingleton<Window.Window>();

        var gl = GL.GetApi(glfwApi.Context());

        gl.Enable(GLEnum.DepthTest);

        glfw.GetFramebufferSize(window.nativeHandle, out int w, out int h);
        gl.Viewport(new Rectangle(0, 0, w, h));

        glfw.SwapInterval(0);

        glfw.SetFramebufferSizeCallback(window.nativeHandle, (_, w, h) => {
            window.size = new Vector2(w, h);
            gl.Viewport(new Rectangle(0, 0, w, h));
        });
    } 


    public override void Register(World world) {
        SetupRenderer(world);
    }
}