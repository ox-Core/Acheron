#include <print>

#include "acheron.hpp"

using namespace acheron;

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    // stages that exist by default are Start, PreUpdate, Update, and PostUpdate

    // register systems, this should be pretty self-explanatory
    world.RegisterSystem([](ecs::World& world) {
        std::println("This is in the start stage");
    }, "Start");

    world.RegisterSystem([](ecs::World& world) {
        std::println("This is in the pre update stage");
    }, "PreUpdate");

    // this is the default, you can omit the stage here
    world.RegisterSystem([](ecs::World& world) {
        std::println("This is in the update stage");
    }, "Update");

    world.RegisterSystem([](ecs::World& world) {
        std::println("This is in the post update stage");
    }, "PostUpdate");

    // just iterate 3 times to demonstrate the start system vs the update systems
    int counter = 0;
    while(counter <= 2) {
		world.Update();
		std::println();
		counter++;
    }
}
