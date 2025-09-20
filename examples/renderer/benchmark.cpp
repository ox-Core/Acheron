#include "acheron.hpp"
#include "modules/renderer/quad.hpp"

#include <cmath>
#include <chrono>
#include <thread>

using namespace acheron;

constexpr float SPEED = 150.0f;

struct Velocity {
    float x, y;
};

Color RandomColor() {
    return Color(rand() % 256, rand() % 256, rand() % 256, 255);
}

void SpawnRandom(ecs::World& world) {
    float angle = static_cast<float>(rand()) / RAND_MAX * 2.0f * 3.14159265f;
    float vx = std::cos(angle) * SPEED;
    float vy = std::sin(angle) * SPEED;

    world.SpawnWith<renderer::RenderableQuad, Transform2D, Velocity>(
        renderer::RenderableQuad {
            100,
            100,
            Color(0xffffffff)
        },
        Transform2D {
            .position = {(float)(rand() % 400), (float)(rand() % 300)},
            .rotation = 0,
            .scale = {1, 1}
        },
        Velocity {
            .x = vx,
            .y = vy
        }
    );
}

int main() {
    auto world = ecs::World();

    world.Import<window::WindowModule>();
    world.Import<renderer::RendererModule>();

    world.RegisterComponent<Velocity>();

    for(int i = 0; i < 500; i++) SpawnRandom(world);

    auto& window = world.GetSingleton<window::Window>();

    world.RegisterSystem<renderer::RenderableQuad, Transform2D, Velocity>(
        [](ecs::World& world, ecs::Entity entity, double dt) {
            auto& transform = world.GetComponent<Transform2D>(entity);
            auto& quad = world.GetComponent<renderer::RenderableQuad>(entity);
            auto& velocity = world.GetComponent<Velocity>(entity);

            auto& window = world.GetSingleton<window::Window>();

            transform.position.x += velocity.x * dt;
            transform.position.y += velocity.y * dt;

            if(transform.position.x < 0) {
                transform.position.x = 0;
                velocity.x *= -1;
                quad.color = RandomColor();
            } else if(transform.position.x + quad.width > window.width) {
                transform.position.x = window.width - quad.width;
                velocity.x *= -1;
                quad.color = RandomColor();
            }

            if(transform.position.y < 0) {
                transform.position.y = 0;
                velocity.y *= -1;
                quad.color = RandomColor();
            } else if(transform.position.y + quad.height > window.height) {
                transform.position.y = window.height - quad.height;
                velocity.y *= -1;
                quad.color = RandomColor();
            }
        }
    );

    auto lastTime = std::chrono::high_resolution_clock::now();
    float timer = 0;

    while(!window.shouldClose) {
        auto currentTime = std::chrono::high_resolution_clock::now();
        std::chrono::duration<float> elapsed = currentTime - lastTime;
        lastTime = currentTime;

        float dt = elapsed.count();
        timer += dt;
        if(timer > 0.5) {
            timer = 0;
            std::println("{} FPS", 1.0 / dt);
        }
        world.Update(dt);
    }
}
