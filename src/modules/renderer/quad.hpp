#pragma once

#include "agl/const.hpp"
#include "agl/func.hpp"
#include "types/basic.hpp"
#include "acheron.hpp"

namespace acheron::renderer {
    /**
     * @brief Component for creating Quads to be rendered
     */
    struct RenderableQuad {
        float width, height;
        Color color = Color(0xffffffff);
    };

    /**
     * @brief Converts RenderableQuad to Mesh2D
     */
    void QuadToMeshSystem(ecs::World& world, ecs::Entity entity);
}
