#pragma once

#include <cstddef>

namespace acheron::renderer {
    /**
     * @brief Base class for all meshes
     */
    struct Mesh {
        unsigned int vao;
        unsigned int vbo;
        unsigned int ebo;

        size_t vertCount;
    };

    /**
     * @brief 2D mesh
     */
    struct Mesh2D: public Mesh {};

    /**
     * @brief Instanced 2D mesh for batch rendering
     */
    struct InstancedMesh2D: public Mesh2D {};

    /**
     * @brief 3D mesh
     */
    struct Mesh3D: public Mesh {};
}
