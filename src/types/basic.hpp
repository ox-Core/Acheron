#pragma once

#include "matrix4.hpp"
#include <array>
#include <cstdint>

namespace acheron {
    struct Color {
        std::uint8_t r, g, b, a;

        constexpr Color() : r(0), g(0), b(0), a(255) {}
        constexpr Color(std::uint8_t r, std::uint8_t g, std::uint8_t b, std::uint8_t a) : r(r), g(g), b(b), a(a) {}
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

    struct Vector3 {
        float x, y, z;
    };

    struct Vector2 {
        float x, y;
    };

    struct Transform2D {
        Vector2 position{0.0f, 0.0f};
        float rotation{0.0f};
        Vector2 scale{1.0f, 1.0f};

        Matrix4 ToMatrix4() const {
            return Matrix4::Translate(position.x, position.y, 0) *
                   Matrix4::RotateZ(rotation) *
                   Matrix4::Scale(scale.x, scale.y, 1);
        }
    };

    struct Transform3D {
        // TODO NOT IMPLIMENTED
    };
};
