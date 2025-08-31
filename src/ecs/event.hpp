#pragma once

#include "types.hpp"

#include <any>
#include <cassert>
#include <functional>
#include <typeindex>
#include <unordered_map>
#include <vector>

namespace acheron::ecs {
    class World;

    /**
     * @brief Manages subscribing, dispatching, and creating events
     *
     * Events are plain structs that can be emitted with Emit<T>(), and
     * subscribed to with Subscribe<T>(). The bus stores events until
     * Dispatch() is called, at which point all subscribers are notified.
     */
    class EventManager {
        public:
        /**
        * @brief Function signature for event callbacks
        * @tparam T The event type
        */
        template<typename T>
        using Callback = std::function<void(World&, const T&)>;

        /**
        * @brief Subscribe to an event type
        *
        * Registers a callback that is invoked whenever the event type is emitted
        *
        * @tparam T The event type to subscribe to
        * @param callback The function that handles the event
        */
        template<typename T>
        void Subscribe(World& world, Callback<T> callback) {
            auto& vec = subscribers[typeid(T)];
            vec.push_back([&world, callback](const std::any& e) {
                callback(world, std::any_cast<const T&>(e));
            });
        }

        /**
        * @brief Emit an event
        *
        * Queues an event of type T for dispatch at the end of the frame
        *
        * @tparam T The event type
        * @param event The event instance
        */
        template<typename T>
        void Emit(const T& event) {
            events[typeid(T)].push_back(event);
        }

        /**
        * @brief Dispatch all queued events
        *
        * Invokes all callbacks subscribed to the emitted event types,
        * then clears the queue
        */
        void Dispatch() {
            for (auto& [type, vec] : events) {
                auto& subs = subscribers[type];
                for (auto& e : vec) {
                    for (auto& sub : subs) {
                        sub(e);
                    }
                }
            }
            events.clear();
        }

        private:
        std::unordered_map<std::type_index, std::vector<std::any>> events; ///< Stored events, grouped by type
        std::unordered_map<std::type_index, std::vector<std::function<void(const std::any&)>>> subscribers; ///< Subscriber callbacks
    };

    /**
     * @brief Generic component lifecycle event (Add/Remove/Set)
     *
     * Example usage:
     * @code
     * world.EmitEvent(ComponentAdded<Position>{entity, &pos});
     * @endcode
     */
    template<typename T>
    struct ComponentAdded {
        Entity entity;
        T* component;
    };

    template<typename T>
    struct ComponentRemoved {
        Entity entity;
    };

    template<typename T>
    struct ComponentSet {
        Entity entity;
        T* component;
    };
}
