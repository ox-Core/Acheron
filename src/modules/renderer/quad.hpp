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
        float width, height; ///< Dimensions of the quad
        Color color = Color(0xffffffff); ///< Color of the quad, will set the materials color
    };

    /**
     * @brief Converts RenderableQuad to Mesh2D
     *
     * @param world World reference
     * @param entity Entity to convert to a Mesh2D
     *
     */
    void QuadToMeshSystem(ecs::World& world, ecs::Entity entity);
}
