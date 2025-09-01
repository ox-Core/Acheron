#include "entity.hpp"

#include <cassert>
#include <print>

using namespace acheron::ecs;

EntityManager::EntityManager() : idCounter(0) {}

Entity EntityManager::Spawn() {
    Entity entity;
    if (!availableEntities.empty()) {
        entity = availableEntities.front();
        availableEntities.pop();
    } else {
        entity = idCounter++;
    }

    signatures[entity] = {};

    return entity;
}

void EntityManager::Despawn(Entity entity) {
    auto it = signatures.find(entity);
    assert(it != signatures.end() && "Entity does not exist");
    it->second.clear();
    signatures.erase(it);

    availableEntities.push(entity);
}

void EntityManager::SetSignature(Entity entity, Signature signature) {
    signatures[entity] = signature;
}

Signature EntityManager::GetSignature(Entity entity) {
    auto it = signatures.find(entity);
    assert(it != signatures.end() && "Entity does not exist");
    return it->second;
}
