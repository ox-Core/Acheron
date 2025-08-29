#include <print>

#include "acheron.hpp"

using namespace acheron;

// systems derive from the base class acheron::ecs::System, and upate is called every frame.

struct StartSystem : public ecs::System {
    void Update(ecs::World& world, double dt) override {
        std::println("This is in the start stage");
    }
};

struct PreUpdateSystem : public ecs::System {
    void Update(ecs::World& world, double dt) override {
        std::println("This is in the pre update stage");
    }
};

struct UpdateSystem : public ecs::System {
    void Update(ecs::World& world, double dt) override {
        std::println("This is in the update stage");
    }
};

struct PostUpdateSystem : public ecs::System {
    void Update(ecs::World& world, double dt) override {
        std::println("This is in the post update stage");
    }
};

int main() {
    // create the world, this is a wrapper that handles entity, component, and system managers
    auto world = ecs::World();

    // use an empty signature, always called.
    world.RegisterSystem<StartSystem>({}, ecs::SystemStage::Start);
    world.RegisterSystem<PreUpdateSystem>({}, ecs::SystemStage::PreUpdate);
    world.RegisterSystem<UpdateSystem>({}, ecs::SystemStage::Update);
    world.RegisterSystem<PostUpdateSystem>({}, ecs::SystemStage::PostUpdate);

    // just iterate 3 times to demonstrate the start system vs the update systems
    int counter = 0;
    while(counter <= 2) {
		world.Update();
		std::println();
		counter++;
    }
}
