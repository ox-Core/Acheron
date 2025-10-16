using System.Drawing;
using System.Numerics;
using Acheron.Core.ECS;
using Acheron.Engine.Window;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Renderer;

public class GLApi {
    private readonly GL? api;

    public GLApi() { }

    public GLApi(GL api) {
        this.api = api;
    }

    public GL GL() {
        return api!;
    }
}

public class ClearColor {
    readonly Color color;

    public ClearColor() {}

    public ClearColor(Color color) {
        this.color = color;
    }

    public static implicit operator Color(ClearColor self) {
        return self.color;
    }
};

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

        world.SetSingleton<GLApi>(new(gl));
    }


    [System("PreRender")]
    public static void ClearFrameSystem(World world) {
        var gl = world.GetSingleton<GLApi>().GL();
        var color = world.GetSingleton<ClearColor>();

        gl.ClearColor(color);
        gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
    }

    [System("PostRender")]
    public static unsafe void PostRenderSystem(World world) {
        var glfw = world.GetSingleton<GLFWApi>().Glfw();
        var nativeHandle = world.GetSingleton<Window.Window>().nativeHandle;
        glfw.SwapBuffers(nativeHandle);
    }

    public override void Register(World world) {
        world.AddStageAfter("PreRender", "PostUpdate");
        world.AddStageAfter("Render3D", "PreRender");
        world.AddStageAfter("Render2D", "Render3D");
        world.AddStageAfter("PostRender", "Render2D");

        SetupRenderer(world);

        if(!world.IsSingletonSet<ClearColor>()) {
            world.SetSingleton<ClearColor>(new(Color.Black));
        }
    }
}