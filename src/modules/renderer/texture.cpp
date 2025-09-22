#include "texture.hpp"

#include <print>
#include <stdexcept>

#define STB_IMAGE_IMPLEMENTATION
#include "stb/stb_image.h"
#include "agl/agl.hpp"

using namespace acheron::renderer;
using namespace acheron::agl;

Texture2D Texture2D::Load(std::string path) {
    int width, height, channels;
    unsigned char* data = stbi_load(path.c_str(), &width, &height, &channels, 4);

    if(!data) {
        std::println("[Acheron] Failed to load texture: {}", path);
        return Texture2D();
    }
    std::println("[Acheron] Loaded texture '{}' at size {}x{} with {} channels", path, width, height, channels);

    unsigned int handle = 0;

    aglGenTextures(1, &handle);
    std::println("[Acheron] Generated OpenGL texture {}", handle);
    aglBindTexture(TEXTURE_2D, handle);

    aglTexImage2D(TEXTURE_2D, 0, RGBA8, width, height, 0, RGBA, GL_UNSIGNED_BYTE, data);

    aglTexParameteri(TEXTURE_2D, TEXTURE_MIN_FILTER, LINEAR);
    aglTexParameteri(TEXTURE_2D, TEXTURE_MAG_FILTER, LINEAR);

    aglTexParameteri(TEXTURE_2D, TEXTURE_WRAP_S, REPEAT);
    aglTexParameteri(TEXTURE_2D, TEXTURE_WRAP_T, REPEAT);


    stbi_image_free(data);

    return Texture2D {
        .handle = handle,
        .width = width,
        .height = height,
        .loaded = true,
    };
}
