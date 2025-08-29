#include <print>

#include "acheron.hpp"
#include <chrono>

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
    auto& should_quit = world.GetSingleton<ShouldQuit>();

    // game loop
    float dt = 0.0;
    while(!should_quit.value) {
        auto startTime = std::chrono::high_resolution_clock::now();

		world.Update(dt);

		auto stopTime = std::chrono::high_resolution_clock::now();

		dt = std::chrono::duration<float, std::chrono::seconds::period>(stopTime - startTime).count();
    }
}
