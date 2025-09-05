#pragma once

#include "acheron.hpp"

namespace acheron::window {
    /**
     * @brief The config for the window
     */
    struct WindowConfig {
        int width = 1280; ///< The windows width
        int height = 720; ///< The windows height
        std::string title = "Acheron"; ///< The windows title
    };

    /**
     * @brief Singleton that is updated every frame
     */
    struct Window {
        int width; ///< The windows width, updated when resized
        int height; ///< The windows width, updated when resized
        std::string title; ///< The windows title, updated when changed

        bool shouldClose = false; ///< If the window should close, use for game loop
        void* nativeHandle = nullptr; ///< Handle to the native window
    };

    /**
     * @brief Module to import to create the window
     */
    struct WindowModule : ecs::Module {
        void Register(ecs::World& world) override;
    };
}
