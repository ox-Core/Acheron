#include "system.hpp"
#include "ecs/types.hpp"

#include <cassert>

using namespace acheron::ecs;

void SystemManager::EntityDespawned(Entity entity) {
    for(auto const& pair : systems) {
        auto const& system = pair.second;
        system->entities.erase(entity);
    }
}

void SystemManager::EntitySignatureChanged(Entity entity, Signature signature) {
    for(auto const& pair : systems) {
        auto const& type = pair.first;
        auto const& system = pair.second;
        auto const systemSignature = signatures[type];

        if((signature & systemSignature) == systemSignature)
            system->entities.insert(entity);
        else
            system->entities.erase(entity);
    }
}
