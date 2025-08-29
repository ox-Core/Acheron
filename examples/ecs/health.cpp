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

// global variable for quit incase health is zero
bool quit = false;

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
            if(health.value <= 0) quit = true;
        }
    }
};

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers.
    auto world = ecs::World();

    world.RegisterComponent<Player>();
    world.RegisterComponent<Health>();

    world.RegisterSystem<HealthSystem>();
    world.SetSystemSignature<HealthSystem>(world.MakeSignature<Player, Health>());

    auto player = world.Spawn();
    world.AddComponent(player, Player{});
    world.AddComponent(player, Health{ 20.0 });

    float dt = 0.0;
    while(!quit) {
        auto startTime = std::chrono::high_resolution_clock::now();

		world.Update(dt);

		auto stopTime = std::chrono::high_resolution_clock::now();

		dt = std::chrono::duration<float, std::chrono::seconds::period>(stopTime - startTime).count();
    }
}
