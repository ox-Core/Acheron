using Acheron.Core.ECS;
using Silk.NET.GLFW;

namespace Acheron.Engine.Window;

public class Window {
    public bool shouldClose = false;
    public unsafe WindowHandle* nativeHandle;
}

class GLFWApi {
    public Glfw api;
}

public class WindowConfig {
    public int width = 1280;
    public int height = 720;
    public string title = "Acheron";
    public bool resizeable = true;
} 

public class WindowModule : Module {
    private readonly Glfw glfw = Glfw.GetApi();

    private unsafe void SetupWindow(World world) {
        var config = world.GetSingleton<WindowConfig>();
        if (!glfw.Init())
            throw new InvalidOperationException("Failed to initialize GLFW");

        glfw.WindowHint(WindowHintBool.Visible, true);
        glfw.WindowHint(WindowHintBool.Resizable, config.resizeable);

        var handle = glfw.CreateWindow(config.width, config.height, config.title, null, null);
        if (handle == null)
            throw new InvalidOperationException("Failed to create GLFW window");

        glfw.MakeContextCurrent(handle);
        glfw.SwapBuffers(handle);

        world.SetSingleton<Window>(new() {
            nativeHandle = handle,
        });

        world.SetSingleton<GLFWApi>(new() {
            api = glfw,
        });
    }

    private static unsafe void PollWindow(World world) {
        var window = world.GetSingleton<Window>();
        var glfw = world.GetSingleton<GLFWApi>().api;
        
        glfw.PollEvents();

        window.shouldClose = glfw.WindowShouldClose(window.nativeHandle);
    }

    public override void Register(World world) {
        if (!world.IsSingletonSet<WindowConfig>())
            world.SetSingleton<WindowConfig>(new());

        SetupWindow(world);

        world.RegisterSystem(PollWindow);
    }
}