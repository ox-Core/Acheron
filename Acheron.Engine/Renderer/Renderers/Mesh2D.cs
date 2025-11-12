using System.Numerics;
using Acheron.Core.ECS;
using Acheron.Engine.Types;
using Silk.NET.Core;
using Silk.NET.OpenGL;

namespace Acheron.Engine.Renderer.Renderers;

class Mesh2DRenderer {
    [System<Mesh2D, Material>("Render2D")]
    static unsafe void RenderMesh2D(World world, Entity e, ref Mesh2D mesh, ref Material material) {
        var window = world.GetSingleton<Window.Window>();
        var transform = AMatrix4.Identity();;
        if (world.HasComponent<Transform2D>(e))
            transform = world.GetComponent<Transform2D>(e).ToMatrix4();

        var shader = material.Shader;

        if (!shader.Valid() || !shader.IsCompiled()) {
            return;
        }

        shader.Bind();

        shader.SetUniform("u_Color", ColorWrapper.ToVec4(material.Color));
        shader.SetUniform("u_ViewProj", AMatrix4.OrthoTopLeft(window.Size.X, window.Size.Y));
        shader.SetUniform("u_Model", transform);

        var gl = world.GetSingleton<GLApi>().GL();

        if (material.Texture.Loaded && material.Texture.Handle != 0) {
            gl.ActiveTexture(GLEnum.Texture0);
            gl.BindTexture(GLEnum.Texture2D, material.Texture.Handle);
            shader.SetUniform("u_Texture", 0);
            shader.SetUniform("u_UseTexture", true);
        } else {
            shader.SetUniform("u_UseTexture", false);
        }

        gl.BindVertexArray(mesh.vao);
        gl.DrawElements(GLEnum.Triangles, mesh.vertCount, GLEnum.UnsignedInt, (void*)0);
    }
}