#include <print>

#include "acheron.hpp"

using namespace acheron;

struct TestComponent {
    bool dummy = false;
};

int main() {
    auto world = ecs::World();

    std::println("Register start");
    world.RegisterComponent<TestComponent>();
    std::println("Register end");

    std::println("spawn start");
    auto e = world.Spawn();
    std::println("component add");
    world.AddComponent<TestComponent>(e);
    std::println("component add done");

    std::println("spawn end");
}
