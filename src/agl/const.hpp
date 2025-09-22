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

    constexpr unsigned int TEXTURE_2D = 0x0DE1;

    constexpr unsigned int RGBA8 = 0x8058;
    constexpr unsigned int RGBA = 0x1908;

    constexpr unsigned int TEXTURE_MIN_FILTER = 0x2801;
    constexpr unsigned int TEXTURE_MAG_FILTER = 0x2800;
    constexpr unsigned int LINEAR = 0x2601;
    constexpr unsigned int REPEAT = 0x2901;
    constexpr unsigned int TEXTURE_WRAP_S = 0x2802;
    constexpr unsigned int TEXTURE_WRAP_T = 0x2803;

    constexpr unsigned int TEXTURE0 = 0x84C0;
    constexpr unsigned int TEXTURE1 = 0x84C1;
    constexpr unsigned int TEXTURE2 = 0x84C2;
    constexpr unsigned int TEXTURE3 = 0x84C3;
    constexpr unsigned int TEXTURE4 = 0x84C4;
    constexpr unsigned int TEXTURE5 = 0x84C5;
    constexpr unsigned int TEXTURE6 = 0x84C6;
    constexpr unsigned int TEXTURE7 = 0x84C7;
    constexpr unsigned int TEXTURE8 = 0x84C8;
}
