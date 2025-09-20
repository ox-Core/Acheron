#include "world.hpp"

#include "../types/basic.hpp"
#include "../types/matrix4.hpp"

#include <print>

using namespace acheron::ecs;

World::World() {
    entityManager = std::make_unique<EntityManager>();
    componentManager = std::make_unique<ComponentManager>();
    systemManager = std::make_unique<SystemManager>();
    eventManager = std::make_unique<EventManager>();

    systemManager->GetOrCreateStage("Start");
    systemManager->StageAfter("PreUpdate", "Start");
    systemManager->StageAfter("Update", "PreUpdate");
    systemManager->StageAfter("PostUpdate", "Update");

    systemManager->PopulateStages();

    RegisterComponent<Color>();
    RegisterComponent<Vector2>();
    RegisterComponent<Vector3>();
    RegisterComponent<Transform2D>();
    RegisterComponent<Transform3D>();
}

void World::AddStageBefore(std::string name, std::string after) {
    systemManager->StageBefore(name, after);
    systemManager->PopulateStages();
}

void World::AddStageAfter(std::string name, std::string before) {
    systemManager->StageAfter(name, before);
    systemManager->PopulateStages();
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
    for (SystemStageID stage : systemManager->orderedStages) {
        if (stage == systemManager->GetStageOrFail("Start") && hasStarted) continue;

        for (auto& system : systemManager->stageSystems[stage]) {
            system->Update(*this, dt);
        }
    }

    hasStarted = true;

    eventManager->Dispatch();
}
