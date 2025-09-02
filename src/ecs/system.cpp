#include "system.hpp"
#include "ecs/types.hpp"

#include <print>
#include <cassert>

using namespace acheron::ecs;

void SystemManager::EntityDespawned(Entity entity) {
    for(auto& [_, system] : systems) {
        system->entities.erase(entity);
    }
}

void SystemManager::EntitySignatureChanged(Entity entity, Signature signature) {
    for(auto& [typeName, system] : systems) {
        auto const systemSignature = signatures[typeName];

        auto it = signatures.find(typeName);
        if (it == signatures.end()) continue;

        const auto& sysSig = it->second;
        bool match = true;

        for (auto comp : sysSig) {
            if (signature.find(comp) == signature.end()) {
                match = false;
                break;
            }
        }

        if (match) system->entities.insert(entity);
        else       system->entities.erase(entity);

    }
}
