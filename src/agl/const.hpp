#pragma once

namespace acheron::agl {
    constexpr unsigned int COLOR_BUFFER_BIT = 0x00004000;
    constexpr unsigned int DEPTH_BUFFER_BIT = 0x00000100;

    constexpr unsigned int ARRAY_BUFFER = 0x8892;
    constexpr unsigned int ELEMENT_ARRAY_BUFFER = 0x8893;
    constexpr unsigned int STATIC_DRAW = 0x88E4;
    constexpr unsigned int DYNAMIC_DRAW = 0x88E8;

    constexpr unsigned int GL_BYTE = 0x1400;
    constexpr unsigned int GL_UNSIGNED_BYTE = 0x1401;
    constexpr unsigned int GL_SHORT = 0x1402;
    constexpr unsigned int GL_UNSIGNED_SHORT = 0x1403;
    constexpr unsigned int GL_INT = 0x1404;
    constexpr unsigned int GL_UNSIGNED_INT = 0x1405;
    constexpr unsigned int GL_FLOAT = 0x1406;

    constexpr unsigned int TRIANGLES = 0x0004;

    constexpr unsigned int COMPILE_STATUS = 0x8B81;

    constexpr unsigned int VERTEX_SHADER = 0x8B31;
    constexpr unsigned int FRAGMENT_SHADER = 0x8B30;
    constexpr unsigned int LINK_STATUS = 0x8B82;

    constexpr unsigned int VERSION = 0x1F02;
    constexpr unsigned int RENDERER = 0x1F02;
    constexpr unsigned int SHADING_LANGUAGE_VERSION = 0x8B8C;
}
