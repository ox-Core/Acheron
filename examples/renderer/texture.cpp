#include "modules/renderer/texture.hpp"
#include "../../src/acheron.hpp"

#include <chrono>
#include <cmath>
#include <thread>

using namespace acheron;

int main() {
    auto world = ecs::World();

    // window configs are set by default, you can do this though if you want to change some proporties
    // world.SetSingleton<window::WindowConfig>({});

    // this will create the window
    world.Import<window::WindowModule>();
    world.Import<renderer::RendererModule>();

    world.SpawnWith<renderer::RenderableQuad, Transform2D, renderer::Texture2D>(
        renderer::RenderableQuad {
            1280.0/2.0,
            720.0/2.0,
            Color(0xffffffff)
        },
        Transform2D {
            .position = {(1280.0 / 2.0) / 2.0, (720.0 / 2.0) / 2.0},
            .rotation = 0,
            .scale = {1, 1}
        },
        renderer::Texture2D::Load("assets/image-1.jpg")
    );

    // the window is set as a global singleton
    auto& window = world.GetSingleton<window::Window>();

    while(!window.shouldClose) {
        world.Update();
        std::this_thread::sleep_for(std::chrono::milliseconds(2)); // limit fps
    }
}
