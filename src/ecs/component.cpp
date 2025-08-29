#include "component.hpp"

using namespace acheron::ecs;

void ComponentManager::EntityDespawned(Entity entity) {
    for(auto const& pair : componentArrays) {
        auto const& component = pair.second;
        component->EntityDespawned(entity);
    }
}
