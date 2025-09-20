#pragma once

#include "agl/const.hpp"
#include "agl/func.hpp"
#include "types/basic.hpp"
#include "acheron.hpp"

namespace acheron::renderer {
    struct RenderableQuad {
        float width, height;
        Color color = Color(0xffffffff);
    };

    void QuadToMeshSystem(ecs::World& world, ecs::Entity entity);
}
