#pragma once

namespace acheron::ecs {
    class World;

    /**
     * @brief The base struct a Module should inherit
     */
    struct Module {
        virtual ~Module() {};
        /**
         * @brief Called when a module is imported
         *
         * @param world A reference to the World
         */
        virtual void Register(World& world) = 0;
    };
}
