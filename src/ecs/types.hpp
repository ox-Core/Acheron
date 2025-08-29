#pragma once

#include <cstdint>
#include <unordered_set>

namespace acheron {
    namespace ecs {
        using Entity = std::uint64_t;

        using ComponentID = std::uint16_t;
        const Entity INVALID_ENTITY = 0;

        using Signature = std::unordered_set<ComponentID>;
    }
}
