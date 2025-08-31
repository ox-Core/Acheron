#pragma once

#include <algorithm>
#include <cassert>

namespace acheron::ecs {
    template<typename T>
    class SingletonStorage {
    public:
        static void Set(T value) {
            instance() = std::move(value);
            initialized() = true;
        }

        static T& Get() {
            assert(initialized() && "Singleton not set!");
            return instance();
        }

    private:
        static T& instance() {
            static T obj;
            return obj;
        }

        static bool& initialized() {
            static bool flag = false;
            return flag;
        }
    };
}
