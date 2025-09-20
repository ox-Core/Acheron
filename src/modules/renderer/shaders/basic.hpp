#pragma once

namespace acheron::renderer::shaders {
    /**
     * @brief Basic shader for rendering meshes
     */
    struct BasicShader {
        static constexpr const char* vertex = R"(
#version 410 core
layout (location = 0) in vec3 aPos;

uniform mat4 u_Model;
uniform mat4 u_ViewProj;

void main() {
    gl_Position = u_ViewProj * u_Model * vec4(aPos, 1.0);
}
        )";

        static constexpr const char* fragment = R"(
#version 410 core
out vec4 FragColor;

uniform vec4 u_Color;

void main() {
    FragColor = u_Color;
}
        )";
    };
}
