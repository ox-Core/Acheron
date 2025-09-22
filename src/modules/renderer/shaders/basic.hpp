#pragma once

namespace acheron::renderer::shaders {
    /**
     * @brief Basic shader for rendering meshes
     */
    struct BasicShader {
        static constexpr const char* vertex = R"(
#version 410 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 inUV;

out vec2 uv;

uniform mat4 u_Model;
uniform mat4 u_ViewProj;

void main() {
    uv = inUV;
    gl_Position = u_ViewProj * u_Model * vec4(aPos, 1.0);
}
        )";

        static constexpr const char* fragment = R"(
#version 410 core
out vec4 FragColor;

in vec2 uv;

uniform vec4 u_Color;
uniform bool u_UseTexture;
uniform sampler2D u_Texture;

void main() {
    if(u_UseTexture) {
        FragColor = texture(u_Texture, uv) * u_Color;
    } else {
        FragColor = u_Color;
    }
}
        )";
    };
}
