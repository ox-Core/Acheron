#pragma once

#include "types.hpp"

#include <cassert>
#include <memory>
#include <set>
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
            std::set<Entity> entities;

            virtual void Update(World& world, double dt) {}
        };

        class SystemManager {
            public:

            template<typename T>
            std::shared_ptr<T> RegisterSystem(SystemStage stage = SystemStage::Update)  {
                const char* typeName = typeid(T).name();

                assert(systems.find(typeName) == systems.end() && "Duplicate system registration");

                auto system = std::make_shared<T>();
                systems[typeName] = system;
                stageSystems[stage].push_back(system);

                return system;
            }

            template<typename T>
            void SetSignature(Signature signature)  {
                const char* typeName = typeid(T).name();

                assert(systems.find(typeName) != systems.end() && "System used before registration");

                signatures[typeName] = signature;
            }

            void EntityDespawned(Entity entity);
            void EntitySignatureChanged(Entity entity, Signature signature);

            std::unordered_map<SystemStage, std::vector<std::shared_ptr<System>>> stageSystems;

            private:
            std::unordered_map<const char*, std::shared_ptr<System>> systems;
            std::unordered_map<const char*, Signature> signatures;
        };
    }
}
