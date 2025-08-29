#pragma once

#include <cstdint>
#include <queue>
#include <unordered_map>

#include "types.hpp"

namespace acheron {
    namespace ecs {
        class EntityManager {
            public:
            EntityManager();
            Entity Spawn();
            void Despawn(Entity entity);
            void SetSignature(Entity entity, Signature signature);
            Signature GetSignature(Entity entity);

            private:
            std::queue<Entity> availableEntities;
            std::unordered_map<Entity, Signature> signatures;
            std::uint32_t idCounter = 0;
        };
    };
};
