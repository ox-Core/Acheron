#include "renderer.hpp"

#include "agl/agl.hpp"
#include "agl/func.hpp"
#include "modules/window/window.hpp"

using namespace acheron::ecs;
using namespace acheron::window;
using namespace acheron::renderer;
using namespace acheron::agl;

void SetupRenderer(World& world) {
    auto& window = world.GetSingleton<Window>();

    auto* handle = reinterpret_cast<GLFWwindow*>(window.nativeHandle);
    glfwMakeContextCurrent(handle);

    aglLoad();

    glfwSwapInterval(1);

    world.SetSingleton<Renderer>({ .initialized = true });
}

void ClearFrame(World& world) {
    auto& window = world.GetSingleton<Window>();
    auto* handle = reinterpret_cast<GLFWwindow*>(window.nativeHandle);

    auto color = world.GetSingleton<ClearColor>().toFloat();

    aglClearColor(color[0], color[1], color[2], color[3]);
    aglClear(COLOR_BUFFER_BIT);

    glfwSwapBuffers(handle);
}

void RendererModule::Register(World& world) {
    world.AddStageAfter("PreRender", "PostUpdate");
    world.AddStageAfter("Render3D", "PreRender");
    world.AddStageAfter("Render2D", "Render3D");

    world.RegisterSystem(SetupRenderer, "Start");
    world.RegisterSystem(ClearFrame, "PreRender");

    if(!world.IsSingletonSet<ClearColor>()) {
        world.SetSingleton(ClearColor());
    }
}
