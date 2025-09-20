#pragma once

#include "types.hpp"

#include <print>
#include <vector>
#include <cassert>
#include <unordered_map>

namespace acheron::ecs {
    /**
     * @brief Interface for component arrays
     */
    struct IComponentArray {
        virtual ~IComponentArray() = default;
        /**
         * @brief Called when an entity is despawned
         */
        virtual void EntityDespawned(Entity entity) {}
    };

    /**
     * @brief Container for components
     *
     * This class handles the association with entities and components, and holds the data for components
     *
     * @tparam T The component type
     */
    template<typename T>
    class ComponentArray : public IComponentArray {
        public:

        /**
         * @brief Add a component and its data to an entity
         *
         * @param entity The entity to apply the component to
         * @param component The component to apply to the entity
         *
         * @throws Assert Fail if the entity already has component T on it
         */
        void InsertData(Entity entity, T component) {
            assert(entityToIndex.find(entity) == entityToIndex.end() && "Duplicate components on entity");

            size_t newIndex = componentArray.size();
            entityToIndex[entity] = newIndex;
            indexToEntity[newIndex] = entity;
            componentArray.push_back(std::move(component));
        }

        /**
         * @brief Checks if en entity has a component
         *
         * @param entity The entity to check
         *
         * @return If component has entity
         */
        bool HasData(Entity entity) const {
            return entityToIndex.find(entity) != entityToIndex.end();
        }

        /**
         * @brief Removes a component and its data associated with an entity
         *
         * @param entity The entity to remove the component from
         *
         * @throws Assert Fail if the component doesnt exist on the entity
         */
        void RemoveData(Entity entity) {
            assert(entityToIndex.find(entity) != entityToIndex.end() && "Removal called for component that doesn't exist");

            size_t indexOfRemoved = entityToIndex[entity];
            size_t indexOfLast = componentArray.size() - 1;

            componentArray[indexOfRemoved] = std::move(componentArray[indexOfLast]);

            Entity entityOfLast = indexToEntity[indexOfLast];
            entityToIndex[entityOfLast] = indexOfRemoved;
            indexToEntity[indexOfRemoved] = entityOfLast;

            entityToIndex.erase(entity);
            indexToEntity.erase(indexOfLast);
            componentArray.pop_back();
        }

        /**
         * @brief Gets component data from an entity
         *
         * @param entity Entity to retrieve the data from
         *
         * @return The component data associated with the entity
         */
        T& GetData(Entity entity) {
            assert(entityToIndex.find(entity) != entityToIndex.end() && "Trying to get Component that doesn't exist");
            return componentArray[entityToIndex[entity]];
        }

        /**
         * @brief Called when an entity is despawned to remove all its component data
         *
         * @param entity Entity that was despawned
         */
        void EntityDespawned(Entity entity) override {
            if (entityToIndex.find(entity) != entityToIndex.end()) {
                RemoveData(entity);
            }
        }

        private:
        std::vector<T> componentArray; ///< List of component data
        std::unordered_map<Entity, size_t> entityToIndex; ///< Map to associate an entity to its component index
        std::unordered_map<size_t, Entity> indexToEntity; ///< Map to associate a component index to an entity
    };
}
