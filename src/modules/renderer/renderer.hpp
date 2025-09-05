#pragma once

#include "acheron.hpp"
#include "types.hpp"

namespace acheron::renderer {
    struct Renderer {
        bool initialized = false;
    };

    struct ClearColor : public Color {};
    /**
     * @brief Module to import to create the window
     */
    struct RendererModule : ecs::Module {
        void Register(ecs::World& world) override;
    };
}
