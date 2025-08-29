#pragma once

#include "types.hpp"

#include <cassert>
#include <memory>
#include <set>
#include <unordered_map>

namespace acheron {
    namespace ecs {
        class World;

        class System {
            public:
            std::set<Entity> entities;

            virtual void Update(World& world, double dt) {}
        };

        class SystemManager {
            public:

            template<typename T>
            std::shared_ptr<T> RegisterSystem()  {
                const char* typeName = typeid(T).name();

                assert(systems.find(typeName) == systems.end() && "Duplicate system registration");

                auto system = std::make_shared<T>();
                systems.insert({typeName, system});
                return system;
            }

            template<typename T>
            void SetSignature(Signature signature)  {
                const char* typeName = typeid(T).name();

                assert(systems.find(typeName) != systems.end() && "System used before registration");

                signatures.insert({typeName, signature});
            }

            void EntityDespawned(Entity entity);
            void EntitySignatureChanged(Entity entity, Signature signature);

            std::unordered_map<const char*, std::shared_ptr<System>> systems;

            private:
            std::unordered_map<const char*, Signature> signatures;
        };
    }
}
