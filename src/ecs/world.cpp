#include "world.hpp"

using namespace acheron::ecs;

World::World() {
    entityManager = std::make_unique<EntityManager>();
    componentManager = std::make_unique<ComponentManager>();
    systemManager = std::make_unique<SystemManager>();
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
    for(auto const& pair : systemManager->systems) {
        auto const& system = pair.second;
        system->Update(*this, dt);
    }
}
