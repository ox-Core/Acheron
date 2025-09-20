#pragma once

#include "acheron.hpp"

#include "material.hpp"
#include "shader.hpp"
#include "types/basic.hpp"

namespace acheron::renderer {
    struct Renderer {
        bool initialized = false;

        Shader basic2DShader;
        Shader instanced2DShader;
    };

    struct ClearColor : public Color {};

    /**
     * @brief Module to import to create the window
     */
    struct RendererModule : ecs::Module {
        void Register(ecs::World& world) override;
    };
}
