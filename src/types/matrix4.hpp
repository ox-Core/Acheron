#pragma once

#include <cmath>

#pragma once

#include <cmath>
#include <cstddef>

namespace acheron {
    /**
     * @brief 4x4 Matrix data structure
     */
    struct Matrix4 {
        float data[16] = {};

        float& operator[](std::size_t idx) { return data[idx]; }
        const float& operator[](std::size_t idx) const { return data[idx]; }

        operator float*() { return data; }
        operator const float*() const { return data; }

        Matrix4 operator*(const Matrix4& other) const {
            return Matrix4::Multiply(*this, other);
        }

        static Matrix4 Identity() {
            Matrix4 m{};
            m[0] = m[5] = m[10] = m[15] = 1.0f;
            return m;
        }

        static Matrix4 Translate(float x, float y, float z) {
            Matrix4 m = Identity();
            m[12] = x;
            m[13] = y;
            m[14] = z;
            return m;
        }

        static Matrix4 Scale(float sx, float sy, float sz) {
            Matrix4 m = Identity();
            m[0] = sx;
            m[5] = sy;
            m[10] = sz;
            return m;
        }

        static Matrix4 RotateZ(float radians) {
            float c = std::cos(radians);
            float s = std::sin(radians);
            Matrix4 m = Identity();
            m[0] = c;  m[1] = s;
            m[4] = -s; m[5] = c;
            return m;
        }

        static Matrix4 Multiply(const Matrix4& a, const Matrix4& b) {
            Matrix4 result{};
            for(int row = 0; row < 4; ++row) {
                for(int col = 0; col < 4; ++col) {
                    result[col + row * 4] =
                        a[0 + row * 4] * b[col + 0 * 4] +
                        a[1 + row * 4] * b[col + 1 * 4] +
                        a[2 + row * 4] * b[col + 2 * 4] +
                        a[3 + row * 4] * b[col + 3 * 4];
                }
            }
            return result;
        }

        static Matrix4 Ortho(float width, float height) {
            Matrix4 m{};
            m[0] = 2.0f / width;
            m[5] = 2.0f / height;
            m[10] = -1.0f;
            m[12] = -1.0f;
            m[13] = -1.0f;
            m[15] = 1.0f;
            return m;
        }

        static Matrix4 OrthoTopLeft(float width, float height) {
            Matrix4 m{};
            m[0] = 2.0f / width;
            m[5] = -2.0f / height;
            m[10] = -1.0f;
            m[12] = -1.0f;
            m[13] = 1.0f;
            m[15] = 1.0f;
            return m;
        }
    };

}
