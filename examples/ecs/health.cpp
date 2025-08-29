#include <print>
#include <chrono>

#include "acheron.hpp"

using namespace acheron;

// tag struct for player
struct Player {};

// actual health component
struct Health {
    float value;
};

struct ShouldQuit {
    bool value = false;
};

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    // register components like this
    world.RegisterComponent<Player>();
    world.RegisterComponent<Health>();

    // systems can be created like this in a lambda. they take in the world, and an entity
    // optionally you can add dt as the last argument for delta time
    world.RegisterSystem<Player, Health>([](ecs::World& world, ecs::Entity entity) {
        // components are BY REFERENCE, so they can be modified and changed
        auto& health = world.GetComponent<Health>(entity);
        health.value -= 1;
        std::println("health: {}", health.value);
        if(health.value <= 0) world.GetSingleton<ShouldQuit>().value = true;
    });

    // create global singleton for when the game should quit
    world.SetSingleton<ShouldQuit>({});

    // create an entity and add the components on it
    auto player = world.Spawn();

    // player is just a tag struct
    world.AddComponent(player, Player{});
    world.AddComponent(player, Health{ 20.0 });

    // this returns a reference so when its changed it will update this, no need to call it every frame
    auto& shouldQuit = world.GetSingleton<ShouldQuit>();

    // game loop
    auto lastTime = std::chrono::high_resolution_clock::now();

    while(!shouldQuit.value) {
        auto currentTime = std::chrono::high_resolution_clock::now();
        std::chrono::duration<float> elapsed = currentTime - lastTime;
        lastTime = currentTime;

        float dt = elapsed.count();
        world.Update(dt);
    }
}
