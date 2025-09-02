#include "acheron.hpp"

using namespace acheron;

int main() {
    auto world = ecs::World();

    // window configs are set by default, you can do this though if you want to change some proporties
    // world.SetSingleton<window::WindowConfig>({});

    // this will create the window
    world.Import<window::WindowModule>();

    // its set as a global singleton
    auto& window = world.GetSingleton<window::Window>();

    // game loop
    while(!window.shouldClose) {
        world.Update();
    }
}
