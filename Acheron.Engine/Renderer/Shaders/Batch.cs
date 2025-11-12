namespace Acheron.Engine.Renderer.Shaders;

public static class BatchShaderSource {
    public const string Vertex = @"
#version 410 core
layout (location = 0) in vec3 aPos;
layout (location = 1) in vec2 inUV;
layout (location = 2) in vec4 aColor;

uniform mat4 u_ViewProj;

out vec2 uv;
out vec4 color;

void main() {
    uv = inUV;
    gl_Position = u_ViewProj * vec4(aPos, 1.0);
    color = aColor;
}
    ";

    public const string Fragment = @"
#version 410 core
out vec4 FragColor;

in vec2 uv;
in vec4 color;

uniform bool u_UseTexture;
uniform sampler2D u_Texture;

void main() {
    if(u_UseTexture) {
        FragColor = texture(u_Texture, uv) * color;
    } else {
        FragColor = color;
    }
}
    ";
}