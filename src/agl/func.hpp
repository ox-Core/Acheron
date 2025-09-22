#pragma once

#include <cstddef>

namespace acheron::agl {
    using ClearColorProc = void (*)(float r, float g, float b, float a);
    using ClearProc = void (*)(unsigned int mask);

    using ViewportProc = void (*)(int x, int y, int w, int h);

    using GenVertexArraysProc = void (*)(int n, unsigned int* arrays);
    using BindVertexArrayProc = void (*)(unsigned int array);
    using GenBuffersProc = void (*)(int n, unsigned int* buffers);
    using BindBufferProc = void (*)(int target, unsigned int buffer);
    using BufferDataProc = void (*)(int target, int size, const void* data, int usage);

    using VertexAttribPointerProc = void (*)(unsigned int index, int size, int type, bool normalized, int stride, const void* pointer);
    using VertexAttribDivisorProc = void (*)(unsigned int index, unsigned int divisor);
    using EnableVertexAttribArrayProc = void (*)(unsigned int index);

    using DrawArraysProc = void (*)(int mode, int first, int count);
    using DrawElementsProc = void (*)(int mode, int count, int type, const void* indices);
    using DrawElementsInstancedProc = void (*)(int mode, int count, int type, const void* indices, int instanceCount);

    using CreateShaderProc = unsigned int (*)(int shaderType);
    using ShaderSourceProc = void (*)(unsigned int shader, int count, const char** string, int* length);
    using CompileShaderProc = void (*)(unsigned int shader);
    using GetShaderivProc = void (*)(unsigned int shader, int pname, int* params);
    using GetShaderInfoLogProc = void (*)(unsigned int shader, int maxLength, int* length, char* log);
    using CreateProgramProc = unsigned int (*)();
    using AttachShaderProc = void (*)(unsigned int program, unsigned int shader);
    using LinkProgramProc = void (*)(unsigned int program);
    using GetProgramivProc = void (*)(unsigned int program, int pname, int* params);
    using GetProgramInfoLogProc = void (*)(unsigned int program, int maxLength, int* length, char* log);
    using DeleteShaderProc = void (*)(unsigned int shader);
    using UseProgramProc = void (*)(unsigned int program);

    using GetUniformLocationProc = int (*)(unsigned int program, const char* name);
    using Uniform4fvProc = void (*)(unsigned int location, int count, const float* value);
    using Uniform1iProc = void (*)(unsigned int location, int value);
    using UniformMatrix4fvProc = void (*)(unsigned int location, int count, bool transpose, const float* value);

    using GetStringProc = const char* (*)(int name);

    using GenTexturesProc = void (*)(int n, unsigned int* textures);
    using BindTextureProc = void (*)(int target, unsigned int textures);
    using TexImage2DProc = void (*)(int target, int level, int internalformat, int width, int height, int border, int format, int type, const void* data);

    using TexParameteriProc = void (*)(int target, int pname, int param);
    using ActiveTextureProc = void (*)(int texture);

    inline ClearColorProc aglClearColor = nullptr;
    inline ClearProc aglClear = nullptr;
    inline ViewportProc aglViewport = nullptr;
    inline GenVertexArraysProc aglGenVertexArrays = nullptr;
    inline BindVertexArrayProc aglBindVertexArray = nullptr;
    inline GenBuffersProc aglGenBuffers = nullptr;
    inline BindBufferProc aglBindBuffer = nullptr;
    inline BufferDataProc aglBufferData = nullptr;

    inline VertexAttribPointerProc aglVertexAttribPointer = nullptr;
    inline VertexAttribDivisorProc aglVertexAttribDivisor = nullptr;
    inline EnableVertexAttribArrayProc aglEnableVertexAttribArray = nullptr;

    inline DrawArraysProc aglDrawArrays = nullptr;
    inline DrawElementsProc aglDrawElements = nullptr;
    inline DrawElementsInstancedProc aglDrawElementsInstanced = nullptr;

    inline CreateShaderProc aglCreateShader = nullptr;
    inline ShaderSourceProc aglShaderSource = nullptr;
    inline CompileShaderProc aglCompileShader = nullptr;
    inline GetShaderivProc aglGetShaderiv = nullptr;
    inline GetShaderInfoLogProc aglGetShaderInfoLog = nullptr;
    inline CreateProgramProc aglCreateProgram = nullptr;
    inline AttachShaderProc aglAttachShader = nullptr;
    inline LinkProgramProc aglLinkProgram = nullptr;
    inline GetProgramivProc aglGetProgramiv = nullptr;
    inline GetProgramInfoLogProc aglGetProgramInfoLog = nullptr;
    inline DeleteShaderProc aglDeleteShader = nullptr;
    inline UseProgramProc aglUseProgram = nullptr;

    inline GetUniformLocationProc aglGetUniformLocation = nullptr;
    inline Uniform4fvProc aglUniform4fv = nullptr;
    inline Uniform1iProc aglUniform1i = nullptr;
    inline UniformMatrix4fvProc aglUniformMatrix4fv = nullptr;

    inline GetStringProc aglGetString = nullptr;

    inline GenTexturesProc aglGenTextures = nullptr;
    inline BindTextureProc aglBindTexture = nullptr;
    inline TexImage2DProc aglTexImage2D = nullptr;

    inline TexParameteriProc aglTexParameteri = nullptr;
    inline ActiveTextureProc aglActiveTexture = nullptr;
}
