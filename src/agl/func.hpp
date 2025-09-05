#pragma once

namespace acheron::agl {
    using ClearColorProc = void (*)(float r, float g, float b, float a);
    using ClearProc = void (*)(unsigned int mask);
    using ViewportProc = void (*)(int x, int y, int w, int h);

    inline ClearColorProc aglClearColor = nullptr;
    inline ClearProc aglClear = nullptr;
    inline ViewportProc aglViewport = nullptr;
}
