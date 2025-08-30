/** \file file.h
 * A brief file description.
 * A more elaborated file description.
 */

#include <print>
#include <chrono>
#include <thread>

#include "acheron.hpp"

using namespace acheron;

/// components that hold data

struct ShouldQuit {
    bool value = false;
};

struct FPSCounter {
    double timer = 0.0;
};

int main() {
    /// create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    /// make sure you register the component before usage, it will throw an assertion if not.
    world.RegisterComponent<FPSCounter>();

    /// create global singleton for when the game should quit
    world.SetSingleton<ShouldQuit>({});

    /// systems can be created like this in a lambda. they take in the world, and an entity
    /// optionally you can add dt as the last argument for delta time
    world.RegisterSystem([](ecs::World& world, ecs::Entity entity, double dt) {
        auto& fpsCounter = world.GetComponent<FPSCounter>(entity);
        auto& timer = fpsCounter.timer; // can do this too

        timer += dt;
        if(timer > 0.5) {
            std::println("FPS: {}", 1.0 / dt);
            timer = 0;
        }
    });

    /// create fps counter and add the component to it
    auto fpsCounter = world.Spawn();
    /// if there isnt anything passed, the default constructor will be called.
    world.AddComponent<FPSCounter>(fpsCounter);

    /// this returns a reference so when its changed it will update this, no need to call it every frame
    auto& shouldQuit = world.GetSingleton<ShouldQuit>();

    auto lastTime = std::chrono::high_resolution_clock::now();

    while(!shouldQuit.value) {
        auto currentTime = std::chrono::high_resolution_clock::now();
        std::chrono::duration<float> elapsed = currentTime - lastTime;
        lastTime = currentTime;

        float dt = elapsed.count();
        world.Update(dt);

        std::this_thread::sleep_for(std::chrono::milliseconds(16)); // limit to ~60fps
    }
}
