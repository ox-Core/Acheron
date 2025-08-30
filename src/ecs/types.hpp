#pragma once

#include <cstdint>
#include <unordered_set>

namespace acheron::ecs {
    using Entity = std::uint64_t;

    using ComponentID = std::uint16_t;
    using Signature = std::unordered_set<ComponentID>;
}
