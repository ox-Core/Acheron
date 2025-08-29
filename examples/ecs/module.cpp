#include <print>

#include "acheron.hpp"

using namespace acheron;

struct ExampleModule : public ecs::Module {
    void Register(ecs::World& world) override {
        std::println("Registering example module");

        world.RegisterSystem([](ecs::World& world, ecs::Entity e) {
            std::println("hello hello");
        });
    }
};

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    // use an empty signature, always called.
    world.Import<ExampleModule>();

    // just iterate 3 times to demonstrate the start system vs the update systems
    int counter = 0;
    while(counter <= 2) {
		world.Update();
		counter++;
    }
}
