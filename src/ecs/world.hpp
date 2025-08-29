#pragma once

#include "ecs/types.hpp"
#include "system.hpp"
#include "component.hpp"
#include "entity.hpp"
#include <memory>

namespace acheron {
    namespace ecs {
        class World {
            public:
            World();
            Entity Spawn();
            void Despawn(Entity entity);

            template<typename T>
            void RegisterComponent() {
                componentManager->RegisterComponent<T>();
            }

            template<typename T>
            void AddComponent(Entity entity, T component) {
                componentManager->AddComponent<T>(entity, component);

                auto signature = entityManager->GetSignature(entity);
                signature.set(componentManager->GetComponentID<T>(), true);
                entityManager->SetSignature(entity, signature);

                systemManager->EntitySignatureChanged(entity, signature);
            }

            template<typename T>
            void RemoveComponent(Entity entity) {
                componentManager->RemoveComponent<T>(entity);

                auto signature = entityManager->GetSignature(entity);
                signature.set(componentManager->GetComponentID<T>(), false);
                entityManager->SetSignature(entity, signature);

                systemManager->EntitySignatureChanged(entity, signature);
            }

            template<typename T>
            T& GetComponent(Entity entity)  {
                return componentManager->GetComponent<T>(entity);
            }

            template<typename T>
            ComponentID GetComponentID() {
                return componentManager->GetComponentID<T>();
            }

            template<typename... Components>
            Signature MakeSignature() {
                Signature signature;
                (signature.set(GetComponentID<Components>()), ...);  // Set each component bit in the signature
                return signature;
            }

            template<typename T>
            std::shared_ptr<T> RegisterSystem() {
                return systemManager->RegisterSystem<T>();
            }

            template<typename T>
            void SetSystemSignature(Signature signature) {
                systemManager->SetSignature<T>(signature);
            }

            void Update(double dt);

            private:
            std::unique_ptr<EntityManager> entityManager;
            std::unique_ptr<ComponentManager> componentManager;
            std::unique_ptr<SystemManager> systemManager;
        };
    }
}
