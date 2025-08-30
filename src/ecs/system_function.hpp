#pragma once

#include "ecs/system.hpp"
#include "ecs/world.hpp"

#include <functional>
#include <type_traits>
#include <utility>

namespace acheron::ecs {
    template<typename Func>
    class SystemFunction : public System {
        public:
        explicit SystemFunction(Func&& f) : func(std::forward<Func>(f)) {}

        void Update(World& world, double dt) override {
            if (entities.empty()) {
                this->template Call<Func>(world, Entity{}, dt);
            } else {
                for (auto entity : entities) {
                    this->template Call<Func>(world, entity, dt);
                }
            }
        }

        private:
        Func func;

        template<typename F>
        void Call(World& world, Entity entity, double dt) {
            if constexpr (std::is_invocable_v<F, World&, Entity, double>) {
                std::invoke(func, world, entity, dt);
            } else if constexpr (std::is_invocable_v<F, World&, Entity>) {
                std::invoke(func, world, entity);
            } else if constexpr (std::is_invocable_v<F, World&, double>) {
                std::invoke(func, world, dt);
            } else if constexpr (std::is_invocable_v<F, World&>) {
                std::invoke(func, world);
            } else {
                static_assert(always_false<F>::value, "SystemFunction callable signature not supported");
            }
        }

        template<class>
        struct always_false : std::false_type {};
    };
}
