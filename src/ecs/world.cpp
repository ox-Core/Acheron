#include "world.hpp"

#include <print>

using namespace acheron::ecs;

World::World() {
    entityManager = std::make_unique<EntityManager>();
    componentManager = std::make_unique<ComponentManager>();
    systemManager = std::make_unique<SystemManager>();
    eventManager = std::make_unique<EventManager>();
}

Entity World::Spawn() {
    return entityManager->Spawn();
}


void World::Despawn(Entity entity) {
    entityManager->Despawn(entity);

    componentManager->EntityDespawned(entity);
    systemManager->EntityDespawned(entity);
}

void World::Update(double dt) {
    if (!hasStarted) {
        for (auto& system : systemManager->stageSystems[SystemStage::Start]) {
            system->Update(*this, dt);
        }
        hasStarted = true;
    }

    for (SystemStage stage : {SystemStage::PreUpdate, SystemStage::Update, SystemStage::PostUpdate}) {
        for (auto& system : systemManager->stageSystems[stage]) {
            system->Update(*this, dt);
        }
    }

    eventManager->Dispatch();
}
