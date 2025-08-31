#include <print>
#include <chrono>
#include <thread>

#include "acheron.hpp"

using namespace acheron;

// components that hold data
struct Health {
    int value = 100;
};

struct ShouldQuit {
    bool value = false;
};

struct DamageTimer {
    double value = 0.0;
};

// events that are emitted and passed into a callback
struct DamageEvent {
    ecs::Entity target;
    int amount;
};

int main() {
    auto world = ecs::World();

    // register components
    world.RegisterComponent<Health>();
    world.RegisterComponent<DamageTimer>();

    // create a global singleton
    world.SetSingleton<ShouldQuit>({});

    // subscribe a system to DamageEvent
    world.SubscribeEvent<DamageEvent>([](ecs::World& world, const DamageEvent& ev) {
        auto& health = world.GetComponent<Health>(ev.target);
        health.value -= ev.amount;

        if (health.value <= 0) {
            std::println("Entity {} died!", ev.target);
        } else {
            std::println("Entity {} took {} damage, new health: {}", ev.target, ev.amount, health.value);
        }
    });

    // system that emits DamageEvent based on each entitys DamageTimer
    world.RegisterSystem<Health, DamageTimer>([](ecs::World& world, ecs::Entity entity, double dt) {
        auto& timer = world.GetComponent<DamageTimer>(entity).value;

        timer += dt;

        if (timer >= 1.0) {
            world.EmitEvent<DamageEvent>({ entity, 5 }); // deal 10 damage
            timer = 0;
        }
    });

    // spawn entities with health and damage timer
    world.Spawn()
        .Add<Health>(75)
        .Add<DamageTimer>();
    world.Spawn()
        .Add<Health>(55)
        .Add<DamageTimer>(0.8);
    world.Spawn()
        .Add<Health>(30)
        .Add<DamageTimer>(0.2);

    auto& shouldQuit = world.GetSingleton<ShouldQuit>();
    auto lastTime = std::chrono::high_resolution_clock::now();

    while (!shouldQuit.value) {
        auto currentTime = std::chrono::high_resolution_clock::now();
        std::chrono::duration<float> elapsed = currentTime - lastTime;
        lastTime = currentTime;

        float dt = elapsed.count();
        world.Update(dt);

        std::this_thread::sleep_for(std::chrono::milliseconds(16));
    }
}
