#pragma once

#include "acheron.hpp"

namespace acheron::renderer {
	/**
	 * @brief System for rendering 2d meshes
	 *
	 * Must contain a Mesh2D, and Material
	 *
	 * @param world World reference
	 * @param world entity Entity to render
	 */
    void RenderMesh2D(ecs::World& world, ecs::Entity entity);
}
