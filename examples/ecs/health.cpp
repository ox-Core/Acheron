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

// systems are defined by a struct that inherit the system base class
struct HealthSystem : public ecs::System {
    // this is the function that is called every frame
    void Update(ecs::World& world, double dt) override {
        // iterate through all entities
        for(auto const& entity : entities) {
            // components are constrained to what the signature is defined as
            // if Player had data i could also call this on Player, it isnt index based
            auto& health = world.GetComponent<Health>(entity);
            health.value -= 1;
            std::println("health: {}", health.value);
            if(health.value <= 0) world.GetSingleton<ShouldQuit>().value = true;
        }
    }
};

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    // register components like this
    world.RegisterComponent<Player>();
    world.RegisterComponent<Health>();

    // systems need to have a signature, its used like a constraint.
    world.RegisterSystem<HealthSystem>();
    world.SetSystemSignature<HealthSystem>(world.MakeSignature<Player, Health>());

    // create global singleton for when the game should quit
    world.SetSingleton<ShouldQuit>({});

    // create an entity and add the components on it
    auto player = world.Spawn();
    // player is just a tag struct
    world.AddComponent(player, Player{});
    world.AddComponent(player, Health{ 20.0 });

    // game loop
    float dt = 0.0;

    // this returns a reference so when its changed it will update this, no need to call it every frame
    auto& should_quit = world.GetSingleton<ShouldQuit>();

    while(!should_quit.value) {
        auto startTime = std::chrono::high_resolution_clock::now();

		world.Update(dt);

		auto stopTime = std::chrono::high_resolution_clock::now();

		dt = std::chrono::duration<float, std::chrono::seconds::period>(stopTime - startTime).count();
    }
}
