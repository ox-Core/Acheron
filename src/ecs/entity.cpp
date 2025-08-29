#include "entity.hpp"

#include <cassert>

using namespace acheron::ecs;

EntityManager::EntityManager() {
    for(Entity e = 0; e < MAX_ENTITIES; e++) {
        availableEntities.push(e);
    }
}

Entity EntityManager::Spawn() {
    assert(aliveCount < MAX_ENTITIES && "Too many entities");

    auto entity = availableEntities.front();
    availableEntities.pop();
    aliveCount++;
    return entity;
}

void EntityManager::Despawn(Entity entity) {
    assert(entity < MAX_ENTITIES && "Entity out of range");
    signatures[entity].reset();

    availableEntities.push(entity);
    aliveCount--;
}

void EntityManager::SetSignature(Entity entity, Signature signature) {
    assert(entity < MAX_ENTITIES && "Entity out of range");
    signatures[entity] = signature;
}

Signature EntityManager::GetSignature(Entity entity) {
    assert(entity < MAX_ENTITIES && "Entity out of range");
    return signatures[entity];
}
