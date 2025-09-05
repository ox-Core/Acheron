#pragma once

#define GLFW_INCLUDE_NONE
#include <GLFW/glfw3.h>

#include <stdexcept>

#include "const.hpp"
#include "func.hpp"

namespace acheron::agl {
    template<typename T>
    T LoadProc(std::string name) {
        T proc = (T)glfwGetProcAddress(name.c_str());
        if(!proc) throw std::runtime_error("Failed to load OpenGL function '" + name + "'");

        return proc;
    }

    inline void aglLoad() {
        aglClearColor = LoadProc<ClearColorProc>("glClearColor");
        aglClear      = LoadProc<ClearProc>     ("glClear");
        aglViewport   = LoadProc<ViewportProc>  ("glViewport");
    }
}
