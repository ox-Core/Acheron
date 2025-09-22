#include "quad.hpp"

#include "ecs/types.hpp"
#include "mesh.hpp"
#include "texture.hpp"

using namespace acheron;

using namespace acheron::renderer;
using namespace acheron::ecs;
using namespace acheron::agl;

void renderer::QuadToMeshSystem(World& world, Entity entity) {
    auto& quad = world.GetComponent<RenderableQuad>(entity);

    if(!world.HasComponent<Material>(entity)) {
        auto& renderer = world.GetSingleton<Renderer>();
        Material mat;
        mat.shader = &renderer.basicShader;
        mat.color = quad.color;

        if(world.HasComponent<Texture2D>(entity))
            mat.textureHandle = world.GetComponent<Texture2D>(entity).handle;

        world.AddComponent<Material>(entity, std::move(mat));
    } else {
        auto& mat = world.GetComponent<Material>(entity);
        mat.color = quad.color;

        if(world.HasComponent<Texture2D>(entity))
            mat.textureHandle = world.GetComponent<Texture2D>(entity).handle;
    }

    if(world.HasComponent<Mesh2D>(entity)) return;

    Mesh2D mesh{};
    mesh.vertCount = 6;

    float verts[] = {
        0.0f, 0.0f, 0.0f, 0.0f, 0.0f,
        quad.width, 0.0f, 0.0f, 1.0f, 0.0f,
        quad.width, quad.height, 0.0f, 1.0f, 1.0f,
        0.0f, quad.height, 0.0f, 0.0f, 1.0f,
    };

    unsigned int indices[] = { 0, 1, 2, 2, 3, 0 };

    aglGenVertexArrays(1, &mesh.vao);
    aglBindVertexArray(mesh.vao);

    aglGenBuffers(1, &mesh.vbo);
    aglBindBuffer(ARRAY_BUFFER, mesh.vbo);
    aglBufferData(ARRAY_BUFFER, sizeof(verts), verts, STATIC_DRAW);

    aglGenBuffers(1, &mesh.ebo);
    aglBindBuffer(ELEMENT_ARRAY_BUFFER, mesh.ebo);
    aglBufferData(ELEMENT_ARRAY_BUFFER, sizeof(indices), indices, STATIC_DRAW);

    aglVertexAttribPointer(0, 3, GL_FLOAT, false, 5 * sizeof(float), (void*)0);
    aglEnableVertexAttribArray(0);
    aglVertexAttribPointer(1, 2, GL_FLOAT, false, 5 * sizeof(float), (void*)(3 * sizeof(float)));
    aglEnableVertexAttribArray(1);

    world.AddComponent<Mesh2D>(entity, std::move(mesh));
}
