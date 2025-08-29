#pragma once

#include "types.hpp"

#include <cassert>
#include <memory>
#include <string>
#include <vector>
#include <unordered_map>

namespace acheron {
    namespace ecs {
        class World;

        enum class SystemStage {
            Start,
            PreUpdate,
            Update,
            PostUpdate
        };

        class System {
            public:
            std::unordered_set<Entity> entities;

            virtual void Update(World& world, double dt) {}
        };

        class SystemManager {
            public:

            template<typename T>
            std::shared_ptr<T> RegisterSystem(SystemStage stage = SystemStage::Update)  {
                auto typeName = typeid(T).name();
                static_assert(std::is_base_of<System, T>::value, "Trying to register system that doesnt inherit System");

                assert(systems.find(typeName) == systems.end() && "Duplicate system registration");

                auto system = std::make_shared<T>();
                systems[typeName] = system;
                stageSystems[stage].push_back(system);

                return system;
            }

            template<typename T>
            void SetSignature(Signature signature)  {
                auto typeName = typeid(T).name();

                assert(systems.find(typeName) != systems.end() && "System used before registration");

                signatures[typeName] = signature;
            }

            void EntityDespawned(Entity entity);
            void EntitySignatureChanged(Entity entity, Signature signature);

            std::unordered_map<SystemStage, std::vector<std::shared_ptr<System>>> stageSystems;
            std::unordered_map<std::string, std::shared_ptr<System>> systems;
            std::unordered_map<std::string, Signature> signatures;
        };
    }
}
