#pragma once

#include <cstddef>

namespace acheron::renderer {
    struct Mesh {
        unsigned int vao;
        unsigned int vbo;
        unsigned int ebo;

        size_t vertCount;
    };

    struct Mesh2D: public Mesh {};
    struct InstancedMesh2D: public Mesh2D {};
    struct Mesh3D: public Mesh {};
}
