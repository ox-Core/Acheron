#pragma once

#include <bitset>
#include <cstdint>

namespace acheron {
    namespace ecs {
        using Entity = std::uint32_t;
        const Entity MAX_ENTITIES = 8192;

        using ComponentID = std::uint8_t;
        const ComponentID MAX_COMPONENTS = 128;

        using Signature = std::bitset<MAX_COMPONENTS>;
    }
}
