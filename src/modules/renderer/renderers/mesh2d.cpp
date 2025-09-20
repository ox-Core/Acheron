#include "mesh2d.hpp"

#include "../mesh.hpp"

using namespace acheron;
using namespace acheron::ecs;
using namespace acheron::renderer;
using namespace acheron::window;

void acheron::renderer::RenderMesh2D(World& world, Entity entity) {
    auto& mesh = world.GetComponent<Mesh2D>(entity);
    auto& material = world.GetComponent<Material>(entity);
    auto& window = world.GetSingleton<Window>();

    Matrix4 transform;
    if(world.HasComponent<Transform2D>(entity)) transform = world.GetComponent<Transform2D>(entity).ToMatrix4();
    else transform = Matrix4::Identity();

    Shader* shader = material.shader;

    if(!shader || !shader->IsCompiled()) {
        // std::println("shader is invalid");
        return;
    }
    shader->Bind();

    shader->SetUniform("u_Color", material.color.toFloat().data());
    shader->SetUniformMat4("u_ViewProj", Matrix4::OrthoTopLeft(window.width, window.height));
    shader->SetUniformMat4("u_Model", transform);

    aglBindVertexArray(mesh.vao);
    aglDrawElements(TRIANGLES, mesh.vertCount, GL_UNSIGNED_INT, 0);
}
