#include <print>

#include "acheron.hpp"

using namespace acheron;

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    // in these cases Entity will be empty, the system is always run, but matched against none.
    world.RegisterSystem([](ecs::World& world, ecs::Entity e) {
        std::println("This is in the start stage");
    }, ecs::SystemStage::Start);

    world.RegisterSystem([](ecs::World& world, ecs::Entity e) {
        std::println("This is in the pre update stage");
    }, ecs::SystemStage::PreUpdate);

    world.RegisterSystem([](ecs::World& world, ecs::Entity e) {
        std::println("This is in the update stage");
    }, ecs::SystemStage::Update);

    world.RegisterSystem([](ecs::World& world, ecs::Entity e) {
        std::println("This is in the post update stage");
    }, ecs::SystemStage::PostUpdate);

    // just iterate 3 times to demonstrate the start system vs the update systems
    int counter = 0;
    while(counter <= 2) {
		world.Update();
		std::println();
		counter++;
    }
}
