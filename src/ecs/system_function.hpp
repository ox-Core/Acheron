#pragma once

#include "ecs/system.hpp"

#include <functional>
#include <type_traits>

namespace acheron {
    namespace ecs {
        template<typename Func>
        class SystemFunction : public System {
            public:
            SystemFunction(Func func) : func(std::move(func)) {}

            void Update(World& world, double dt) override {
                for (auto entity : entities.empty() ? std::initializer_list<Entity>{Entity{}} : entities) {
                    if constexpr (std::is_invocable_v<Func, World&, Entity, double>) {
                        std::invoke(func, world, entity, dt);
                    } else if constexpr (std::is_invocable_v<Func, World&, Entity>) {
                        std::invoke(func, world, entity);
                    } else {
                        static_assert(sizeof(Func) == 0, "System must take (World&, Entity) or (World&, Entity, double)");
                    }
                }
            }

            private:
            Func func;
        };

    }
}
