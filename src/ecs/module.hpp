#pragma once

namespace acheron::ecs {
    class World;

    struct Module {
        virtual ~Module() {};
        virtual void Register(World& world) = 0;
    };
}
