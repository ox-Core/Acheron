#pragma once

#include "ecs/module.hpp"
#include "ecs/system_function.hpp"
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
                auto system = systemManager->RegisterSystem<T>(stage);
                SetSystemSignature<T>(signature);
                return system;
            }

            template<typename Func>
            std::shared_ptr<System> RegisterSystemExplicit(Func&& func, Signature signature = {}, SystemStage stage = SystemStage::Update) {
                using SystemType = SystemFunction<std::decay_t<Func>>;
                auto systemFunc = std::make_shared<SystemType>(std::forward<Func>(func));

                static int counter = 0;
                std::string typeName = "LambdaSystem_" + std::to_string(counter++);

                systemManager->systems[typeName] = systemFunc;
                systemManager->signatures[typeName] = signature;
                systemManager->stageSystems[stage].push_back(systemFunc);

                return systemFunc;
            }

            template<typename... Components, typename Func>
            std::shared_ptr<System> RegisterSystem(Func&& func, SystemStage stage = SystemStage::Update) {
                auto signature = MakeSignature<Components...>();
                return RegisterSystemExplicit(std::forward<Func>(func), signature, stage);
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

            template<typename T>
            void Import() {
                static_assert(std::is_base_of<Module, T>::value, "Import must inherit from Module");

                auto module = T{};
                module.Register(*this);
            }

            void Update(double dt = 0.0);

            private:
            int counter = 0;
            bool hasStarted = false;
            std::unique_ptr<EntityManager> entityManager;
            std::unique_ptr<ComponentManager> componentManager;
            std::unique_ptr<SystemManager> systemManager;
            std::unordered_map<std::type_index, std::any> singletons;
        };
    }
}
