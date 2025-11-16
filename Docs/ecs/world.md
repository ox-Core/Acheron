# World

The World is the central coordinator of the whole ECS. It provides all the interfaces needed to interact with the [Entities](entities.md), [Components](components.md), [Systems](systems.md), [Events](events.md), and [Views](views.md)

To create a new world use
```cs
var world = new World();
```