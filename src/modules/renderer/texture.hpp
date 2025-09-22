#pragma once

#include <string>

namespace acheron::renderer {
    /**
     * @brief 2D Texture
     */
    struct Texture2D {
        unsigned int handle = 0;
        int width = 0, height = 0;

        int loaded = false;

        static Texture2D Load(std::string path);
    };
}
