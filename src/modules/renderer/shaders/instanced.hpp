#pragma once

namespace acheron::renderer::shaders {
    /**
     * @brief Currently unused shader for batch renderer
     */
    struct InstanceShader {
        static constexpr const char* vertex = R"(
#version 410 core
layout(location = 0) in vec3 aPos;
layout(location = 1) in mat4 aModel;
layout(location = 5) in vec4 aColor;

out vec4 vColor;

uniform mat4 u_ViewProj;

void main() {
    gl_Position = u_ViewProj * aModel * vec4(aPos, 1.0);
    vColor = aColor;
}
        )";

        static constexpr const char* fragment = R"(
#version 410 core
in vec4 vColor;
out vec4 FragColor;

void main() {
    FragColor = vColor;
}
        )";
    };
}
