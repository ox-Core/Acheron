#pragma once

#include <print>
#include <string>

#include "agl/agl.hpp"

using namespace acheron::agl;

namespace acheron::renderer {
    /**
     * @brief Wrapper for OpenGL shaders
     */
    struct Shader {
        unsigned int id;

        /**
         * @brief Compile a shader with the source
         *
         * @param vsSrc Vertex Shader Source
         * @param fsSrc Fragment Shader Source
         *
         * @throws Runtime erorr if shader didnt compile properly
         */
        void Compile(std::string vsSrc, std::string fsSrc) {
            auto compileStage = [](unsigned int type, const char* src) {
                unsigned int shader = aglCreateShader(type);
                aglShaderSource(shader, 1, &src, nullptr);
                aglCompileShader(shader);

                int success;

                aglGetShaderiv(shader, COMPILE_STATUS, &success);
                if(!success) {
                    char log[512];
                    aglGetShaderInfoLog(shader, 512, nullptr, log);
                    throw std::runtime_error(log);
                }
                return shader;
            };

            const char* vsSrccstr = vsSrc.c_str();
            const char* fsSrccstr = fsSrc.c_str();
            unsigned int vs = compileStage(VERTEX_SHADER, vsSrccstr);
            unsigned int fs = compileStage(FRAGMENT_SHADER, fsSrccstr);

            id = aglCreateProgram();
            aglAttachShader(id, vs);
            aglAttachShader(id, fs);
            aglLinkProgram(id);

            int success;
            aglGetProgramiv(id, LINK_STATUS, &success);
            if(!success) {
                char log[512];
                aglGetProgramInfoLog(id, 512, nullptr, log);
                throw std::runtime_error(log);
            }

            aglDeleteShader(vs);
            aglDeleteShader(fs);
        }

        /**
         * @brief Bind the shader
         */
        void Bind() {
            aglUseProgram(id);
        }


        /**
         * @brief Sets a vec4 uniform
         *
         * @param name Name of the uniform
         * @param value Value of the uniform
         */
        void SetUniform(const char* name, float value[4]) {
            int location = aglGetUniformLocation(id, name);
            if(location != -1) {
                aglUniform4fv(location, 1, value);
            }
        }

        /**
         * @brief Set a mat4 uniform
         *
         * @param name Name of the uniform
         * @param value Value of the uniform
         */
        void SetUniformMat4(const char* name, const float mat[16]) const {
            int location = aglGetUniformLocation(id, name);
            if (location != -1) aglUniformMatrix4fv(location, 1, false, mat);
        }

        /**
         * @brief Returns if the Shader was compiled
         */
        bool IsCompiled() { return id != 0; }
    };
}
