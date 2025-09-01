#pragma once

#include "types.hpp"
#include "component_array.hpp"

#include <cassert>
#include <memory>
#include <print>
#include <unordered_map>

namespace acheron::ecs {
    /**
     * @brief Manages registered components and its arrays
     */
    class ComponentManager {
        public:

        /**
         * @brief Registers a component type creating a ComponentArray and an ID
         *
         * @tparam T The component type to be registered
         *
         * @throws Assert Fail if the component was already registered
         */
        template<typename T>
        void RegisterComponent() {
            const char* typeName = typeid(T).name();

            std::println("component comp");
            assert(componentTypes.find(typeName) == componentTypes.end() && "Duplicate registration of component");
            std::println("component comp finish");

            componentTypes[typeName] = nextComponentID;
            componentArrays[typeName] = std::make_shared<ComponentArray<T>>();
            nextComponentID++;
        }

        /**
         * @brief Gets the ID of a component
         *
         * @tparam T The component to get the ID for
         *
         * @return The ID for the component
         * @throws Assert Fail if the component wasnt registered yet
         */
        template<typename T>
        ComponentID GetComponentID() {
            const char* typeName = typeid(T).name();
            std::println("component comp id");
            assert(componentTypes.find(typeName) != componentTypes.end() && "Component not registered before use");
            std::println("component comp id finish");

            return componentTypes[typeName];
        }

        /**
         * @brief Adds a component and its data to an entity
         *
         * @tparam T The component type to add to the entity
         * @param entity The entity to add a component and its data for
         * @param component The components data
         *
         */
        template<typename T>
        void AddComponent(Entity entity, T component)  {
            GetComponentArray<T>()->InsertData(entity, component);
        }

        /**
         * @brief Removes a component and its data from an entity
         *
         * @tparam T The component type to remove from the entity
         * @param entity The entity to remove a component and its data for
         */
        template<typename T>
        void RemoveComponent(Entity entity) {
            GetComponentArray<T>()->RemoveData(entity);
        }

        /**
         * @brief Get components data associated with an entity
         *
         * @tparam T The component type to get the data for from an entity
         *
         * @return Component data associated with the entity
         */
        template<typename T>
        T& GetComponent(Entity entity) {
            return GetComponentArray<T>()->GetData(entity);
        }

        /**
         * @brief Remove components associated with an entity
         *
         * @param entity Entity to remove components from
         */
        void EntityDespawned(Entity entity);

        private:
        std::unordered_map<const char*, ComponentID> componentTypes; ///< Map to associate component names and IDs
        std::unordered_map<const char*, std::shared_ptr<IComponentArray>> componentArrays; ///< Map to associate component names and its data array
        ComponentID nextComponentID; ///< counter to assign an ID to a component

        /**
         * @brief Gets a shared pointer for the component array associated with T
         *
         * @tparam T The type of the component to get its array for
         *
         * @return Shared pointer for a component array
         * @throws Assertion Fail if the component wasnt registered yet
         */
        template<typename T>
        std::shared_ptr<ComponentArray<T>> GetComponentArray() {
            const char* typeName = typeid(T).name();
            assert(componentTypes.find(typeName) != componentTypes.end() && "Component not registered before use");
            return std::static_pointer_cast<ComponentArray<T>>(componentArrays[typeName]);
        }
    };
}
