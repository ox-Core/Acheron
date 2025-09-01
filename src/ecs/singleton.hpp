#pragma once

#include <algorithm>
#include <cassert>

namespace acheron::ecs {
    /**
     * @brief Interal storage wrapper for singletons
     *
     * @tparam The type of the singleton
     */
    template<typename T>
    class SingletonStorage {
        public:
        /**
         * @brief Initializes singleton and sets the instance value
         *
         * @param value The value of the singleton
         */
        static void Set(const T& value) {
            instance() = value;
            initialized() = true;
        }

        /**
         * @brief Get the instance of a singleton
         *
         * @return Reference to the singleton instance
         * @throws Assert Fail if the singleton isnt set
         */
        static T& Get() {
            assert(initialized() && "Singleton not set");
            return instance();
        }

        private:
        /**
         * @brief Where the data lives for the instance
         */
        static T& instance() {
            static T obj;
            return obj;
        }

        /**
         * @brief Flag to check if its set or not
         */
        static bool& initialized() {
            static bool flag = false;
            return flag;
        }
    };
}
