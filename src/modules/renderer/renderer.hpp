#pragma once

#include "acheron.hpp"

#include "material.hpp"
#include "shader.hpp"
#include "types/basic.hpp"

namespace acheron::renderer {
    /**
     * @brief Global renderer singleton
     */
    struct Renderer {
        bool initialized = false; ///< Check for when the renderer is initialized, safety

        Shader basicShader; ///< Global shader for basic rendering
        Shader instancedShader; ///< Global shader for instanced rendering
    };

    /**
     * @brief ClearColor singleton, set this to change the window clear color
     */
    struct ClearColor : public Color {};

    /**
     * @brief Module to import to initialize the renderer
     */
    struct RendererModule : ecs::Module {
        void Register(ecs::World& world) override;
    };
}
