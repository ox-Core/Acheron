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

    { std::println("about to call Spawn directly via function pointer"); auto pf = &ecs::World::Spawn; auto e = (world.*pf)(); std::println("returned from pointer call"); }
    std::println("spawn end");
}
