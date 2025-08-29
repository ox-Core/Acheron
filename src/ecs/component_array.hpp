#pragma once

#include "types.hpp"

#include <array>
#include <cassert>
#include <unordered_map>

namespace acheron {
    namespace ecs {
        class IComponentArray {
            public:
            virtual ~IComponentArray() = default;
            virtual void EntityDespawned(Entity entity) {}
        };

        template<typename T>
        class ComponentArray : public IComponentArray {
            public:
            void InsertData(Entity entity, T component) {
                assert(entityToIndex.find(entity) == entityToIndex.end() && "Duplicate components on entity");

                auto newIndex = size;
                entityToIndex[entity] = newIndex;
                indexToEntity[newIndex] = entity;
                componentArray[newIndex] = component;
                size++;
            }

            void RemoveData(Entity entity)  {
                assert(entityToIndex.find(entity) != entityToIndex.end() && "Removal called for component that doesnt exist");

                auto indexOfRemoved = entityToIndex[entity];
                auto indexOfLast = size - 1;
                componentArray[indexOfRemoved] = componentArray[indexOfLast];

                auto entityOfLast = indexToEntity[indexOfLast];
                entityToIndex[entityOfLast] = indexOfRemoved;
                indexToEntity[indexOfRemoved] = entityOfLast;

                entityToIndex.erase(entity);
                indexToEntity.erase(indexOfLast);
                size--;
            }

            T& GetData(Entity entity) {
                assert(entityToIndex.find(entity) != entityToIndex.end() && "Trying to get Component that doesnt exist");
                return componentArray[entityToIndex[entity]];
            }

            void EntityDespawned(Entity entity) override {
                if(entityToIndex.find(entity) != entityToIndex.end()) {
                    RemoveData(entity);
                }
            }

            private:
            std::array<T, MAX_ENTITIES> componentArray;
            std::unordered_map<Entity, size_t> entityToIndex;
            std::unordered_map<size_t, Entity> indexToEntity;
            size_t size;
        };
    }
}
