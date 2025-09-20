#pragma once

#define GLFW_INCLUDE_NONE
#include <GLFW/glfw3.h>

#include <stdexcept>

#include "const.hpp"
#include "func.hpp"

namespace acheron::agl {
    /**
     * @brief Load a function pointer from glfw
     *
     * @tparam T The function type
     * @param name Name of the function in glfw
     */
    template<typename T>
    T LoadProc(std::string name) {
        T proc = (T)glfwGetProcAddress(name.c_str());
        if(!proc) throw std::runtime_error("Failed to load OpenGL function '" + name + "'");

        return proc;
    }

    /**
     * @brief Load binded OpenGL function pointers
     */
    inline void aglLoad() {
        aglClearColor = LoadProc<ClearColorProc>("glClearColor");
        aglClear = LoadProc<ClearProc>("glClear");

        aglViewport = LoadProc<ViewportProc>("glViewport");

        aglGenVertexArrays = LoadProc<GenVertexArraysProc>("glGenVertexArrays");
        aglBindVertexArray = LoadProc<BindVertexArrayProc>("glBindVertexArray");
        aglGenBuffers = LoadProc<GenBuffersProc>("glGenBuffers");
        aglBindBuffer = LoadProc<BindBufferProc>("glBindBuffer");
        aglBufferData = LoadProc<BufferDataProc>("glBufferData");

        aglVertexAttribPointer = LoadProc<VertexAttribPointerProc>("glVertexAttribPointer");
        aglVertexAttribDivisor = LoadProc<VertexAttribDivisorProc>("glVertexAttribDivisor");
        aglEnableVertexAttribArray = LoadProc<EnableVertexAttribArrayProc>("glEnableVertexAttribArray");

        aglDrawArrays = LoadProc<DrawArraysProc>("glDrawArrays");
        aglDrawElements = LoadProc<DrawElementsProc>("glDrawElements");
        aglDrawElementsInstanced = LoadProc<DrawElementsInstancedProc>("glDrawElements");

        aglCreateShader = LoadProc<CreateShaderProc>("glCreateShader");
        aglShaderSource = LoadProc<ShaderSourceProc>("glShaderSource");
        aglCompileShader = LoadProc<CompileShaderProc>("glCompileShader");
        aglGetShaderiv = LoadProc<GetShaderivProc>("glGetShaderiv");
        aglGetShaderInfoLog = LoadProc<GetShaderInfoLogProc>("glGetShaderInfoLog");
        aglCreateProgram = LoadProc<CreateProgramProc>("glCreateProgram");
        aglAttachShader = LoadProc<AttachShaderProc>("glAttachShader");
        aglLinkProgram = LoadProc<LinkProgramProc>("glLinkProgram");
        aglGetProgramiv = LoadProc<GetProgramivProc>("glGetProgramiv");
        aglGetProgramInfoLog = LoadProc<GetProgramInfoLogProc>("glGetProgramInfoLog");
        aglDeleteShader = LoadProc<DeleteShaderProc>("glDeleteShader");
        aglUseProgram = LoadProc<UseProgramProc>("glUseProgram");

        aglGetUniformLocation = LoadProc<GetUniformLocationProc>("glGetUniformLocation");
        aglUniform4fv = LoadProc<Uniform4fvProc>("glUniform4fv");
        aglUniformMatrix4fv = LoadProc<UniformMatrix4fvProc>("glUniformMatrix4fv");

        aglGetString = LoadProc<GetStringProc>("glGetString");
    }
}
