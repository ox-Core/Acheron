#include <print>

#include "acheron.hpp"

using namespace acheron;

void PrintHiSystem(ecs::World& world) {
    std::println("Hi");
}

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    // you can register a system by passing in a function instead of using lambdas
    world.RegisterSystem(PrintHiSystem);

    // just iterate 3 times
    int counter = 0;
    while(counter <= 2) {
		world.Update();
		counter++;
    }
}
