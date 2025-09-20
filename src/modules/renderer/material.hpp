#pragma once

#include "modules/renderer/shader.hpp"

#include "types/basic.hpp"

namespace acheron::renderer {
    /**
     * @brief Material for meshes
     */
    struct Material {
        Shader* shader = nullptr;
        Color color = Color(0xffffffff);
    };
}
