using Acheron.Core.ECS;
using Acheron.Engine.Window;
using Silk.NET.GLFW;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Input;

public record struct KeyPressedEvent(Keys Key, KeyModifiers Mods);
public record struct KeyReleasedEvent(Keys Key, KeyModifiers Mods);
public record struct KeyDownEvent(Keys Key, KeyModifiers Mods);

public record struct MouseMoveEvent(float X, float Y);
public record struct MouseButtonPressedEvent(MouseButton Button);
public record struct MouseButtonReleasedEvent(MouseButton Button);
public record struct MouseButtonDownEvent(MouseButton Button);

public class Keyboard {
    const int KEY_COUNT = (int)Silk.NET.GLFW.Keys.LastKey+1;

    public bool[] Keys = new bool[KEY_COUNT];
    public KeyModifiers[] Mods = new KeyModifiers[KEY_COUNT];
}

public class Mouse {
    public bool[] Buttons = new bool[8];
}

public class InputModule : Module {
    public override Type[] Dependencies =>[typeof(WindowModule)];

    [System("PollInput")]
    private static void PollInput(World world) {
        var glfw = world.GetSingleton<GLFWApi>().Glfw();
        glfw.PollEvents();        
    }

    [System("FireInputEvents")]
    private static void FireInputEvents(World world) {
        var kb = world.GetSingleton<Keyboard>();
        var mouse = world.GetSingleton<Mouse>();

        for (int i = 0; i < mouse.Buttons.Length; i++) {
            if (mouse.Buttons[i])
                world.Emit<MouseButtonDownEvent>(new((MouseButton)i));
        }
        
        for(int i = 0; i < kb.Keys.Length; i++) {
            if (kb.Keys[i])
                world.Emit<KeyDownEvent>(new((Keys)i, kb.Mods[i]));
        }
    }

    private unsafe void SetupCallbacks(World world) {
        ref var window = ref world.GetSingleton<Window.Window>();
        var glfw = world.GetSingleton<GLFWApi>().Glfw();

        glfw.SetCursorPosCallback(window.NativeHandle, (WindowHandle* win, double x, double y) => {
            world.Emit<MouseMoveEvent>(new((float)x, (float)y));
        });

        glfw.SetMouseButtonCallback(window.NativeHandle, (WindowHandle* win, MouseButton button, InputAction action, KeyModifiers mods) => {
            ref var mouse = ref world.GetSingleton<Mouse>();

            if (action == InputAction.Press) {
                mouse.Buttons[(int)button] = true;
                world.Emit<MouseButtonPressedEvent>(new(button));
            }
            if (action == InputAction.Release) {
                mouse.Buttons[(int)button] = false;
                world.Emit<MouseButtonReleasedEvent>(new(button));
            }
        });

        // Silk.NET.GLFW.WindowHandle* window, Silk.NET.GLFW.Keys key, int scanCode, Silk.NET.GLFW.InputAction action, Silk.NET.GLFW.KeyModifiers mods
        glfw.SetKeyCallback(window.NativeHandle, (WindowHandle* window, Keys key, int scanCode, InputAction action, KeyModifiers mods) => {
            ref var kb = ref world.GetSingleton<Keyboard>();
            
            if(action == InputAction.Press) {
                kb.Keys[(int)key] = true;
                world.Emit<KeyPressedEvent>(new(key, mods));
            } else if(action == InputAction.Release) {
                kb.Keys[(int)key] = false;
                world.Emit<KeyReleasedEvent>(new(key, mods));
            }
        });
    }

    public override void Register(World world) {
        world.SetSingleton(new Keyboard());
        world.SetSingleton(new Mouse());

        world.AddStageAfter("PollInput", "Start");
        world.AddStageAfter("FireInputEvents", "PollInput");
        world.AddStageAfter("PreUpdate", "FireInputEvents");

        SetupCallbacks(world);
    }
}