# Input

### Depends on
| Module | Reason | 
| - | - |
| [Window](window.md) | Uses GLFW for Input |

---

This module will use the current Window to poll for input, and fire input [events](../ecs/events.md).

### Event Types
```cs
public record struct KeyPressedEvent(Keys Key, KeyModifiers Mods);
public record struct KeyReleasedEvent(Keys Key, KeyModifiers Mods);
public record struct KeyDownEvent(Keys Key, KeyModifiers Mods);

public record struct MouseMoveEvent(float X, float Y);
public record struct MouseButtonPressedEvent(MouseButton Button);
public record struct MouseButtonReleasedEvent(MouseButton Button);
public record struct MouseButtonDownEvent(MouseButton Button);
```