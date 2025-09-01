#pragma once

#include <queue>
#include <unordered_map>

#include "types.hpp"

namespace acheron::ecs {
    class World;
    /**
     * @brief Manages every entity
     */
    class EntityManager {
        public:
        EntityManager();
        /**
         * @brief Spawns an entity
         *
         * @return An Entity ID associated with the entity just spawned
         */
        Entity Spawn();

        /**
         * @brief Despawn(destroy) an entity
         *
         * @param entity Entity to despawn
         *
         * @throws Assertion Fail if the entity doesnt exist
         */
        void Despawn(Entity entity);

        /**
         * @brief Sets the signature of an entity
         *
         * Sets the signature of the components to be associated with the entity
         * Usually this is called internally by World
         *
         * @param entity The entity to set the signature of
         * @param signature The new signature to apply to the entity
         */
        void SetSignature(Entity entity, Signature signature);

        /**
         * @brief Gets the signature for an entity
         *
         * @param entity The entity to get the signature from
         *
         * @return The signature for the entity passed
         */
        Signature GetSignature(Entity entity);

        private:
        std::vector<Entity> availableEntities; ///< Queue for the available entity IDs
        std::unordered_map<Entity, Signature> signatures; ///< Map for entities and their associated signature
        Entity idCounter = 0; ///< Counter to apply an ID to a newly created entity
    };
};
