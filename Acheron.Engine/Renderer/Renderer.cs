using System.Drawing;
using System.Numerics;
using Acheron.Core.ECS;
using Acheron.Engine.Renderer.Renderers;
using Acheron.Engine.Renderer.Shaders;
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
    public Color Color { get; set; } = Color.Black;

    public ClearColor() { }

    public ClearColor(Color color) => Color = color;

    public static implicit operator Color(ClearColor self) => self.Color;
}

class RendererInternal {
    public RendererInternal() { }

    public BatchRenderer2D? BatchRenderer;
    
    public RendererInternal(World world) {
        this.BatchRenderer = new(world);
    }
}

record struct BasicShader(Shader Shader);
record struct BatchShader(Shader Shader);

public class RendererModule : Module {
    public static unsafe void SetupRenderer(World world) {
        var glfwApi = world.GetSingleton<GLFWApi>();
        var glfw = glfwApi.Glfw();

        var window = world.GetSingleton<Window.Window>();

        var gl = GL.GetApi(glfwApi.Context());

        gl.DebugMessageCallback((GLEnum source, GLEnum type, int id, GLEnum severity, int length, nint message, nint userParam) => {
            Console.WriteLine($"[Acheron DEBUG({severity})]: GL: I HATE YOU");
        }, (void*)0);

        gl.Enable(GLEnum.DepthTest);

        glfw.GetFramebufferSize(window.NativeHandle, out int w, out int h);
        gl.Viewport(new Size(w, h));

        glfw.SwapInterval(0);

        glfw.SetFramebufferSizeCallback(window.NativeHandle, (_, w, h) => {
            window.Size = new Vector2(w, h);
            gl.Viewport(new Size(w, h));
        });

        world.SetSingleton<GLApi>(new(gl));

        world.SetSingleton<BasicShader>(new(
            new Shader(
                gl,
                BasicShaderSource.Vertex,
                BasicShaderSource.Fragment
            )
        ));

        world.SetSingleton<BatchShader>(new(
            new Shader(
                gl,
                BatchShaderSource.Vertex,
                BatchShaderSource.Fragment
            )
        ));

        world.SetSingleton<RendererInternal>(new(world));
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
        var nativeHandle = world.GetSingleton<Window.Window>().NativeHandle;
        glfw.SwapBuffers(nativeHandle);
    }

    public override void Register(World world) {
        world.AddStageAfter("PreRender", "PostUpdate");
        world.AddStageAfter("Render3D", "PreRender");
        world.AddStageAfter("Render2D", "Render3D");
        world.AddStageAfter("PostRender", "Render2D");

        SetupRenderer(world);

        if (!world.IsSingletonSet<ClearColor>()) {
            world.SetSingleton<ClearColor>(new(Color.Black));
        }
    }
}