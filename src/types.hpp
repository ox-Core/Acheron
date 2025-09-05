#pragma once

#include <array>
#include <cstdint>

namespace acheron {
    struct Color {
        std::uint8_t r, g, b, a;

        constexpr Color() : r(0), g(0), b(0), a(255) {}
        constexpr Color(char r, char g, char b, char a) : r(r), g(g), b(b), a(a) {}
        constexpr Color(std::uint32_t hex) {
            r = (hex & 0xff000000) >> 24;
            g = (hex & 0x00ff0000) >> 16;
            b = (hex & 0x0000ff00) >> 8;
            a = (hex & 0x000000ff) >> 0;
        }

        std::array<float,4> toFloat() const {
            return { r / 255.0f, g / 255.0f, b / 255.0f, a / 255.0f };
        }
    };
};
