#include <print>

#include "acheron.hpp"

using namespace acheron;

struct SingletonHello {
    bool hi = false;
};

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    std::println("Singleton Registering...");
    world.SetSingleton<SingletonHello>({});
    std::println("Singleton Registered!");

    std::println("Singleton Retrieving...");
    world.GetSingleton<SingletonHello>();
    std::println("Singleton Retrieved!");
}
