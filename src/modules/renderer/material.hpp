#pragma once

#include "shader.hpp"

#include "texture.hpp"
#include "types/basic.hpp"

namespace acheron::renderer {
    /**
     * @brief Material for meshes
     */
    struct Material {
        Shader* shader = nullptr; ///< Shader pointer for the material
        Color color = Color(0xffffffff); ///< Color to tint the material
        unsigned int textureHandle = 0;
    };
}
