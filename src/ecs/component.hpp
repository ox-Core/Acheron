#pragma once

#include "types.hpp"
#include "component_array.hpp"

#include <cassert>
#include <memory>
#include <unordered_map>

namespace acheron {
    namespace ecs {
        class ComponentManager {
            public:

            template<typename T>
            void RegisterComponent()  {
                const char* typeName = typeid(T).name();

                assert(componentTypes.find(typeName) == componentTypes.end() && "Duplicate registration of component");

                componentTypes[typeName] = nextComponentID;
                componentArrays[typeName] = std::make_shared<ComponentArray<T>>();
                nextComponentID++;
            }

            template<typename T>
            ComponentID GetComponentID() {
                const char* typeName = typeid(T).name();
                assert(componentTypes.find(typeName) != componentTypes.end() && "Component not registered before use");

                return componentTypes[typeName];
            }

            template<typename T>
            void AddComponent(Entity entity, T component)  {
                GetComponentArray<T>()->InsertData(entity, component);
            }

            template<typename T>
            void RemoveComponent(Entity entity) {
                GetComponentArray<T>()->RemoveData(entity);
            }

            template<typename T>
            T& GetComponent(Entity entity) {
                return GetComponentArray<T>()->GetData(entity);
            }

            void EntityDespawned(Entity entity);

            private:
            std::unordered_map<const char*, ComponentID> componentTypes;
            std::unordered_map<const char*, std::shared_ptr<IComponentArray>> componentArrays;
            ComponentID nextComponentID;

            template<typename T>
            std::shared_ptr<ComponentArray<T>> GetComponentArray() {
                const char* typeName = typeid(T).name();
                assert(componentTypes.find(typeName) != componentTypes.end() && "Component not registered before use");
                return std::static_pointer_cast<ComponentArray<T>>(componentArrays[typeName]);
            }
        };
    }
}
