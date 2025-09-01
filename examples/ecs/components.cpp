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
    world.Spawn()
        .Add<TestComponent>();

    std::println("spawn end");
}
