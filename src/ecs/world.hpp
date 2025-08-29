#pragma once

#include "ecs/types.hpp"
#include "system.hpp"
#include "component.hpp"
#include "entity.hpp"
#include <any>
#include <memory>
#include <typeindex>

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
                (signature.set(GetComponentID<Components>()), ...);
                return signature;
            }

            template<typename T>
            std::shared_ptr<T> RegisterSystem(Signature signature = {}, SystemStage stage = SystemStage::Update) {
                return RegisterSystemImpl<T>(stage, signature);
            }

            template<typename T>
            void SetSystemSignature(Signature signature) {
                systemManager->SetSignature<T>(signature);
            }

            template<typename T>
            void SetSingleton(T value) {
                singletons[typeid(T)] = std::move(value);
            }

            template<typename T>
            T& GetSingleton() {
                auto it = singletons.find(typeid(T));
                assert(it != singletons.end() && "Singleton does not exist");
                return std::any_cast<T&>(it->second);
            }

            void Update(double dt = 0.0);

            private:
            bool hasStarted = false;
            std::unique_ptr<EntityManager> entityManager;
            std::unique_ptr<ComponentManager> componentManager;
            std::unique_ptr<SystemManager> systemManager;
            std::unordered_map<std::type_index, std::any> singletons;

            template<typename T>
            std::shared_ptr<T> RegisterSystemImpl(SystemStage stage, Signature signature) {
                auto system = systemManager->RegisterSystem<T>(stage);
                SetSystemSignature<T>(signature);
                return system;
            }
        };
    }
}
