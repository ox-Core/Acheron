#pragma once

#include <array>
#include <cstdint>
#include <queue>

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
            std::array<Signature, MAX_ENTITIES> signatures;
            std::uint32_t aliveCount;
        };
    };
};
