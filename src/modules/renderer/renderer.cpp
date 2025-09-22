#include "renderer.hpp"

#include "agl/agl.hpp"

#include "agl/const.hpp"
#include "mesh.hpp"
#include "modules/renderer/shaders/instanced.hpp"
#include "modules/renderer/texture.hpp"
#include "shaders/basic.hpp"
#include "quad.hpp"

#include "renderers/mesh2d.hpp"

#include "modules/window/window.hpp"

#include <print>

using namespace acheron;
using namespace acheron::ecs;
using namespace acheron::window;
using namespace acheron::renderer;
using namespace acheron::agl;

void SetupRenderer(World& world) {
    auto& window = world.GetSingleton<Window>();

    auto* handle = reinterpret_cast<GLFWwindow*>(window.nativeHandle);
    glfwMakeContextCurrent(handle);

    aglLoad();

    int w, h;
    glfwGetFramebufferSize(handle, &w, &h);
    aglViewport(0, 0, w, h);

    glfwSwapInterval(0);

    glfwSetFramebufferSizeCallback(handle, [](GLFWwindow* win, int width, int height) {
        auto* world = reinterpret_cast<World*>(glfwGetWindowUserPointer(win));
        auto& window = world->GetSingleton<Window>();
        window.width  = width;
        window.height = height;

        aglViewport(0, 0, width, height);
    });

    Renderer renderer{};

    renderer.basicShader.Compile(shaders::BasicShader::vertex, shaders::BasicShader::fragment);
    renderer.instancedShader.Compile(shaders::InstanceShader::vertex, shaders::InstanceShader::fragment);

    renderer.initialized = true;

    world.SetSingleton(std::move(renderer));
}

void ClearFrame(World& world) {
    auto color = world.GetSingleton<ClearColor>().toFloat();

    aglClearColor(color[0], color[1], color[2], color[3]);
    aglClear(COLOR_BUFFER_BIT);
}

void PostRender(World& world) {
    auto& window = world.GetSingleton<Window>();
    auto* handle = reinterpret_cast<GLFWwindow*>(window.nativeHandle);

    glfwSwapBuffers(handle);
}

void RendererModule::Register(World& world) {
    world.RegisterComponent<RenderableQuad>();

    world.RegisterComponent<Mesh>();
    world.RegisterComponent<Mesh2D>();
    world.RegisterComponent<Mesh3D>();
    world.RegisterComponent<Material>();
    world.RegisterComponent<Texture2D>();

    world.AddStageAfter("PreRender", "PostUpdate");
    world.AddStageAfter("Render3D", "PreRender");
    world.AddStageAfter("Render2D", "Render3D");
    world.AddStageAfter("PostRender", "Render3D");

    SetupRenderer(world);

    world.RegisterSystem(ClearFrame, "PreRender");
    world.RegisterSystem(PostRender, "PostRender");

    world.RegisterSystem<RenderableQuad>(QuadToMeshSystem, "PreRender");
    world.RegisterSystem<Mesh2D>(RenderMesh2D, "Render2D");

    if(!world.IsSingletonSet<ClearColor>()) {
        world.SetSingleton(ClearColor());
    }
}
