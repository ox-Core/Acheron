#pragma once

#include "singleton.hpp"
#include "module.hpp"
#include "system_function.hpp"
#include "types.hpp"
#include "event.hpp"
#include "system.hpp"
#include "component.hpp"
#include "entity.hpp"

#include <memory>

namespace acheron::ecs {
    /**
     * @brief Central context for the ECS
     *
     * This class manages EVERYTHING to do with the ECS
    */
    class World {
        public:
        /**
         * @brief Constructs a new World, initializing managers
         */
        World();

        /**
         * @brief Spawns a new entity
         * @return The newly created entity
         */
        Entity Spawn();

        /**
         * @brief Despawns(destroys) an entity
         * Cleans up all associated components and notifies systems
         * @param entity The entity to remove
         */
        void Despawn(Entity entity);

        /**
         * @brief Registers a new component type with the ECS
         * @tparam T The component type to register
         */
        template<typename T>
        void RegisterComponent() {
            componentManager->RegisterComponent<T>();
        }

        /**
         * @brief Adds a component to an entity
         *
         * Also updates the entity's signature and notifies systems
         *
         * @tparam T Component type
         * @param entity The entity receiving the component
         * @param component Optional instance of the component (default constructed if omitted)
         */
         template<typename T, typename... Args>
         void AddComponent(Entity entity, Args&&... args) {
            componentManager->AddComponent<T>(entity, T(std::forward<Args>(args)...));

            auto signature = entityManager->GetSignature(entity);
            signature.insert(componentManager->GetComponentID<T>());
            entityManager->SetSignature(entity, signature);

            systemManager->EntitySignatureChanged(entity, signature);
        }

        /**
         * @brief Removes a component from an entity
         *
         * Updates the entity's signature and notifies systems
         *
         * @tparam T Component type to remove
         * @param entity The entity to modify
         */
        template<typename T>
        void RemoveComponent(Entity entity) {
            componentManager->RemoveComponent<T>(entity);

            auto signature = entityManager->GetSignature(entity);
            signature.erase(componentManager->GetComponentID<T>());
            entityManager->SetSignature(entity, signature);

            systemManager->EntitySignatureChanged(entity, signature);
        }

        /**
         * @brief Retrieves a reference to a component on an entity
         * @tparam T Component type
         * @param entity The entity to query
         * @return Reference to the component
         */
        template<typename T>
        T& GetComponent(Entity entity)  {
            return componentManager->GetComponent<T>(entity);
        }

        /**
         * @brief Retrieves the unique ID associated with a component type
         * @tparam T Component type
         * @return ComponentID for the given type
         */
        template<typename T>
        ComponentID GetComponentID() {
            return componentManager->GetComponentID<T>();
        }

        /**
         * @brief Creates a signature that includes all specified component types
         * @tparam Components... Variadic list of component types
         * @return Signature representing the component set
         */
        template<typename... Components>
        Signature MakeSignature() {
            Signature signature;
            (signature.insert(GetComponentID<Components>()), ...);
            return signature;
        }

        /**
         * @brief Registers a system of type T
         *
         * @tparam T System type (must inherit from System)
         * @param signature The component signature this system cares about (default empty)
         * @param stage The update stage to run this system in (default Update)
         * @return Shared pointer to the created system
         */
        template<typename T>
        std::shared_ptr<T> RegisterSystem(Signature signature = {}, SystemStage stage = SystemStage::Update) {
            auto system = systemManager->RegisterSystem<T>(stage);
            SetSystemSignature<T>(signature);
            return system;
        }

        /**
         * @brief Registers a system explicitly from a function object or lambda
         *
         * Allows flexible system definition without requiring a class
         *
         * @tparam Func Callable type.
         * @param func The system function/lambda
         * @param signature Component signature this system cares about (default empty)
         * @param stage Update stage to run the system in (default Update)
         * @return Shared pointer to the created system
         */
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

        /**
         * @brief Registers a system from a lambda or callable with component type deduction.
         *
         * Example:
         * \code{.cpp}
         * world.RegisterSystem<Position, Velocity>(
         *     [](Entity e, Position& pos, Velocity& vel) { ... }
         * );
         * \endcode
         *
         * @tparam Components... The component types the system operates on
         * @tparam Func Callable type
         * @param func The system function/lambda
         * @param stage The update stage (default Update)
         * @return Shared pointer to the created system
         */
        template<typename... Components, typename Func>
        std::shared_ptr<System> RegisterSystem(Func&& func, SystemStage stage = SystemStage::Update) {
            auto signature = MakeSignature<Components...>();
            return RegisterSystemExplicit(std::forward<Func>(func), signature, stage);
        }

        /**
         * @brief Sets the component signature for a system
         * @tparam T System type
         * @param signature The signature to associate with the system
         */
        template<typename T>
        void SetSystemSignature(Signature signature) {
            systemManager->SetSignature<T>(signature);
        }

        /**
         * @brief Stores a singleton instance in the world
         * @tparam T Singleton type
         * @param value The instance to store
         */
        template<typename T>
        void SetSingleton(T value) {
            SingletonStorage<T>::Set(std::move(value));
        }

        /**
         * @brief Retrieves a singleton instance from the world
         * @tparam T Singleton type
         * @return Reference to the stored singleton
         * @throws Assert failure if the singleton has not been set
         */

        template<typename T>
        T& GetSingleton() {
            return SingletonStorage<T>::Get();
        }

        /**
         * @brief Imports a module into the world
         *
         * The module must inherit from Module
         * @tparam T Module type
         */
        template<typename T>
        void Import() {
            static_assert(std::is_base_of<Module, T>::value, "Import must inherit from Module");

            auto module = T{};
            module.Register(*this);
        }

        /**
         * @brief Subscribes a callback to an event
         *
         * @tparam T Event type
         * @param cb Callback to subscribe to the event
         */
        template<typename T>
        void SubscribeEvent(EventManager::Callback<T> cb) {
            eventManager->Subscribe<T>(*this, cb);
        }

        /**
         * @brief Emit an event to the event queue
         *
         * @tparam T Event type to emit
         * @param event Event and its data
         */
        template<typename T>
        void EmitEvent(const T& event) {
            eventManager->Emit<T>(event);
        }

        /**
         * @brief Dispatch events
         */
        void DispatchEvents() {
            eventManager->Dispatch();
        }

        /**
         * @brief Runs system updates in the world
         *
         * Systems are executed in stage order:
         * - Start (once)
         * - PreUpdate
         * - Update
         * - PostUpdate
         *
         * @param dt Delta time passed to systems (default 0.0)
         */
        void Update(double dt = 0.0);

        private:
        int counter = 0; ///< Internal counter for anonymous systems.
        bool hasStarted = false; ///< Tracks if the Start stage has run.
        std::unique_ptr<EntityManager> entityManager; ///< Manages entity lifecycle.
        std::unique_ptr<ComponentManager> componentManager; ///< Manages component storage.
        std::unique_ptr<SystemManager> systemManager; ///< Manages system execution.
        std::unique_ptr<EventManager> eventManager; ///< Manages system execution.
    };
}
