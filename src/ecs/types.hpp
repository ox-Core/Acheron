#pragma once

#include <cstdint>
#include <unordered_set>

namespace acheron::ecs {
    using Entity = std::uint64_t; ///< Entity ID

    using ComponentID = std::uint16_t; ///< Component ID
    /**
     * @brief A signature that can be applied to Entities and Systems
     *
     * This describes which components are associated with entities and systems.
     */
    using Signature = std::unordered_set<ComponentID>;

    using SystemStageID = std::uint64_t; ///< System Stage ID
}
