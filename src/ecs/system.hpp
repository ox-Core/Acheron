#pragma once

#include "types.hpp"

#include <cassert>
#include <memory>
#include <string>
#include <vector>
#include <unordered_map>

namespace acheron::ecs {
    class World;

    /**
     * @brief Size for the stage ID
     */


    /**
     * @brief Stage/Order in which systems are executed in
     */
    struct SystemStage {
        SystemStageID id;
        std::string name;

        bool operator==(const SystemStage& other) const noexcept {
            return id == other.id;
        }
    };

    /**
     * @brief Systems called every frame based on entities
     */
    class System {
        public:
        /**
         * @brief List of entities a system is matched with
         */
        std::unordered_set<Entity> entities;

        /**
         * @brief This function is overriden with the functionality of the System
         *
         * @param world A reference to the World
         * @param dt Delta Time
         */
        virtual void Update(World& world, double dt) {}
    };

    /**
     * @brief Manages systems and the stages associated with them
     */
    class SystemManager {
        public:

        /**
         * @brief Registers a system
         *
         * Registers a system to its appropriate stage, and type
         * @tparam T The struct that should inherit from System
         * @param stage The stage at which the system should be executed
         *
         * @return A pointer to the system registered(usually ignored)
         * @throws Assertion Fail if T doesnt inherit system, or if already registered
         */
        template<typename T>
        std::shared_ptr<T> RegisterSystem(std::string stage)  {
            auto typeName = typeid(T).name();
            static_assert(std::is_base_of<System, T>::value, "Trying to register system that doesnt inherit System");

            assert(systems.find(typeName) == systems.end() && "Duplicate system registration");

            auto system = std::make_shared<T>();
            systems[typeName] = system;
            stageSystems[nameToID[stage]].push_back(system);

            return system;
        }

        /**
         * @brief Sets a systems signature
         *
         * @tparam T The struct that should inherit from System
         * @param signature The system signature to apply to T
         */
        template<typename T>
        void SetSignature(Signature signature)  {
            auto typeName = typeid(T).name();

            assert(systems.find(typeName) != systems.end() && "System used before registration");

            signatures[typeName] = signature;
        }

        /**
         * @brief Called when an entity is despawned
         *
         * Removes the entity from the entity list that applied to the system
         *
         * @param entity The entity that was despawned
         */
        void EntityDespawned(Entity entity);

        /**
         * @brief Gets a StageID by name, or creates one if it doesnt exist
         *
         * @param name The name of the stage to create or get
         *
         * @return The ID created or of an existing stage
         */
        SystemStageID GetOrCreateStage(const std::string& name);

        /**
         * @brief Gets a StageID by name
         *
         * @param name The name of the stage to get
         *
         * @throws Runtime Error if the stage doesnt exist
         * @return The ID created
         */
        SystemStageID GetStageOrFail(const std::string& name);

        /**
         * @brief Creates a stage before another
         *
         * @param name The name of the system to register before
         * @param after The system that will be after the registered stage
         */
        void StageBefore(std::string name, std::string after);

        /**
         * @brief Creates a stage before another
         *
         * @param name The name of the system to register after
         * @param after The system that will be before the registered stage
         */
        void StageAfter(std::string name, std::string before);

        /**
         * @brief Clears and populates ordereredStages
         */
        void PopulateStages();

        /**
         * @brief Updates the system signature based on the entities new signature
         *
         * @param entity The entities whom system was changed
         * @param signature The new signature of the entity
         */
        void EntitySignatureChanged(Entity entity, Signature signature);

        std::unordered_map<std::string, SystemStageID> nameToID; ///< Maps a stage name to its respective ID
        std::unordered_map<SystemStageID, std::vector<SystemStageID>> edges; ///< How a stage is associated with one another
        std::vector<SystemStage> stages; ///< A list of registered stages
        std::vector<SystemStageID> orderedStages; ///< Stages put in order by PopulateStages

        std::unordered_map<SystemStageID, std::vector<std::shared_ptr<System>>> stageSystems;
        std::unordered_map<std::string, std::shared_ptr<System>> systems; ///< A map of systems and their name
        std::unordered_map<std::string, Signature> signatures; ///< A map of systems name and the signature associated with them

        SystemStageID counter;

        bool per_entity = true;
    };
}
