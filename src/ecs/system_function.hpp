#pragma once

#include "ecs/system.hpp"
#include "ecs/world.hpp"

#include <functional>
#include <type_traits>
#include <utility>

namespace acheron::ecs {
    /**
     * @brief Wrapper class for systems to support lambda and function pointers
     *
     * @tparam Func System callback
     */
    template<typename Func>
    class SystemFunction : public System {
        public:
        explicit SystemFunction(Func&& f) : func(std::forward<Func>(f)) {} ///< System Function Wrapper

        /**
         * @brief Iterates through all entities associated with system, and call it
         *
         * @param world A reference to the World
         * @param dt Delta Time
         */
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

        /**
         * @brief Wrapper to match the signature and call the internal function the SystemFunction wrapped
         *
         * @tparam F The function to match its signature with to confirm its valid
         * @param world A reference to the World
         * @param entity The entity that the system matched with
         * @param dt Delta Time
         *
         * @throws Assert failure if the internal function doesnt match certain requirements
         */
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
