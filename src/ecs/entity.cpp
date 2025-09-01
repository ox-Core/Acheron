#include "entity.hpp"

#include <cassert>
#include <print>

using namespace acheron::ecs;

EntityManager::EntityManager() : idCounter(0) {}

Entity EntityManager::Spawn() {
    assert(idCounter >= 0); // if signed
    Entity entity = Entity(-1);

    if (!availableEntities.empty()) {
        entity = availableEntities.back();
        // quick invariants:
        assert(entity != Entity(-1));
        assert(entity < 1000000000u); // large sentinel to catch garbage
        availableEntities.pop_back();
    } else {
        entity = static_cast<Entity>(idCounter++);
        assert(entity != Entity(-1));
    }

    if (signatures.find(entity) != signatures.end()) {
        // should not happen for a freshly spawned entity
        assert(false && "spawning an entity that already exists in signatures");
    }

    signatures.emplace(entity, Signature{});

    return entity;
}

void EntityManager::Despawn(Entity entity) {
    auto it = signatures.find(entity);
    assert(it != signatures.end() && "Entity does not exist");
    it->second.clear();
    signatures.erase(it);

    availableEntities.push_back(entity);
}

void EntityManager::SetSignature(Entity entity, Signature signature) {
    signatures[entity] = signature;
}

Signature EntityManager::GetSignature(Entity entity) {
    auto it = signatures.find(entity);
    assert(it != signatures.end() && "Entity does not exist");
    return it->second;
}
