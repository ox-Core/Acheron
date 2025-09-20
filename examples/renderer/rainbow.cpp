#include "../../src/acheron.hpp"

#include <cmath>

using namespace acheron;

struct ColorCounter {
    float value = 0;
};

void RainbowClear(ecs::World& world, ecs::Entity entity) {
    auto& t = world.GetComponent<ColorCounter>(entity).value;
    auto& color = world.GetSingleton<renderer::ClearColor>();
    t += 0.01;

    float r = std::sin(t * 0.8f) * 0.5f + 0.5f;
    float g = std::sin(t * 0.6f + 2.0f) * 0.5f + 0.5f;
    float b = std::sin(t * 0.4 + 4.0f) * 0.5f + 0.5f;

    color.r = static_cast<unsigned char>(r * 255.0f);
    color.g = static_cast<unsigned char>(g * 255.0f);
    color.b = static_cast<unsigned char>(b * 255.0f);
    color.a = 255;
}

int main() {
    auto world = ecs::World();

    // window configs are set by default, you can do this though if you want to change some proporties
    // world.SetSingleton<window::WindowConfig>({});

    // this will create the window
    world.Import<window::WindowModule>();
    world.Import<renderer::RendererModule>();

    // register the color counter component
    world.RegisterComponent<ColorCounter>();

    // register the system to change the global clear color
    world.RegisterSystem<ColorCounter>(RainbowClear);

    // create an entity with the color counter
    world.SpawnWith<ColorCounter>();

    // the window is set as a global singleton
    auto& window = world.GetSingleton<window::Window>();

    // game loop
    while(!window.shouldClose) {
        world.Update();
    }
}
