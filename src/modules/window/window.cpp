#include "window.hpp"

#include "agl/agl.hpp"
#include <stdexcept>

using namespace acheron::window;
using namespace acheron::ecs;

void SetupWindow(World& world) {
    auto& config = world.GetSingleton<WindowConfig>();

    if(!glfwInit()) throw std::runtime_error("Failed to initialize GLFW3");

    glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 4);
    glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 1);
    glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);
    glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, true);
    glfwWindowHint(GLFW_VISIBLE, GLFW_TRUE);

    auto windowHandle = glfwCreateWindow(config.width, config.height, config.title.c_str(), nullptr, nullptr);
    if(!windowHandle) throw std::runtime_error("Failed to create GLFW3 Window");

    glfwSetWindowUserPointer(windowHandle, &world);

    glfwMakeContextCurrent(windowHandle);
    glfwSwapBuffers(windowHandle);

    world.GetSingleton<Window>() = Window {
        .width = config.width,
        .height = config.height,
        .title = config.title,
        .shouldClose = false,
        .nativeHandle = windowHandle
    };
}

void PollWindow(World& world) {
    auto& window = world.GetSingleton<Window>();
    auto* handle = reinterpret_cast<GLFWwindow*>(window.nativeHandle);

    glfwPollEvents();

    if(glfwWindowShouldClose(handle)) {
        window.shouldClose = true;
    }
}

void WindowModule::Register(World& world) {
    if(!world.IsSingletonSet<WindowConfig>()) {
        world.SetSingleton<WindowConfig>({});
    }

    world.SetSingleton<Window>({});

    world.RegisterSystem(SetupWindow, "Start");
    world.RegisterSystem(PollWindow);
}
