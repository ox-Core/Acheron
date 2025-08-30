#pragma once

#include "types.hpp"

#include <vector>
#include <cassert>
#include <unordered_map>

namespace acheron::ecs {
    struct IComponentArray {
        virtual ~IComponentArray() = default;
        virtual void EntityDespawned(Entity entity) {}
    };

    template<typename T>
    class ComponentArray : public IComponentArray {
        public:
        void InsertData(Entity entity, T component) {
            assert(entityToIndex.find(entity) == entityToIndex.end() && "Duplicate components on entity");

            size_t newIndex = componentArray.size();
            entityToIndex[entity] = newIndex;
            indexToEntity[newIndex] = entity;
            componentArray.push_back(std::move(component));
        }

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

        T& GetData(Entity entity) {
            assert(entityToIndex.find(entity) != entityToIndex.end() && "Trying to get Component that doesn't exist");
            return componentArray[entityToIndex[entity]];
        }

        void EntityDespawned(Entity entity) override {
            if (entityToIndex.find(entity) != entityToIndex.end()) {
                RemoveData(entity);
            }
        }

        private:
        std::vector<T> componentArray;
        std::unordered_map<Entity, size_t> entityToIndex;
        std::unordered_map<size_t, Entity> indexToEntity;
    };
}
