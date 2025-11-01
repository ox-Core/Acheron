using System.Numerics;
using Acheron.Core.ECS;

using Silk.NET.GLFW;

namespace Acheron.Engine.Window;

public class Window {
    public bool shouldClose = false;
    public Vector2 size = Vector2.Zero;
    public unsafe WindowHandle* nativeHandle;
}

class GLFWApi {
    private readonly Glfw? api;
    private readonly GlfwContext? ctx;

    public GLFWApi() { }

    public GLFWApi(Glfw api, GlfwContext ctx) {
        this.api = api;
        this.ctx = ctx;
    }

    public Glfw Glfw() {
        return api!;
    }

    public GlfwContext Context() {
        return ctx!;
    }
}

public class WindowConfig {
    public int Width = 1280;
    public int Height = 720;
    public string Title = "Acheron";
    public bool Resizeable = true;
}

public class WindowModule : Module {
    private static unsafe void SetupWindow(World world) {
        var glfw = Glfw.GetApi();

        var config = world.GetSingleton<WindowConfig>();
        if (!glfw.Init())
            throw new InvalidOperationException("Failed to initialize GLFW");

        glfw.WindowHint(WindowHintInt.ContextVersionMajor, 4);
        glfw.WindowHint(WindowHintInt.ContextVersionMinor, 1);
        glfw.WindowHint(WindowHintOpenGlProfile.OpenGlProfile, OpenGlProfile.Core);
        glfw.WindowHint(WindowHintBool.OpenGLForwardCompat, true);

        glfw.WindowHint(WindowHintBool.Visible, true);
        glfw.WindowHint(WindowHintBool.Resizable, config.Resizeable);

        var handle = glfw.CreateWindow(config.Width, config.Height, config.Title, null, null);
        if (handle == null)
            throw new InvalidOperationException("Failed to create GLFW window");

        glfw.MakeContextCurrent(handle);
        glfw.SwapBuffers(handle);

        world.SetSingleton<Window>(new() {
            nativeHandle = handle,
            size = new Vector2(config.Width, config.Height)
        });

        world.SetSingleton<GLFWApi>(new(glfw, new GlfwContext(glfw, handle)));
    }

    [System("PreUpdate")]
    private static unsafe void PollWindow(World world) {
        var window = world.GetSingleton<Window>();
        var glfw = world.GetSingleton<GLFWApi>().Glfw();

        glfw.PollEvents();

        window.shouldClose = glfw.WindowShouldClose(window.nativeHandle);
    }

    public override void Register(World world) {
        if (!world.IsSingletonSet<WindowConfig>())
            world.SetSingleton<WindowConfig>(new());

        SetupWindow(world);
    }
}