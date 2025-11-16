# Components

A component is something that is added to an entity.

Components can be used to either tag an entity, or attatch data to an entity.

To create a component you can attatch the attribute to structs as such

```cs
[Component]
struct Component1 {
    float val;
}

[Component]
record struct Component2(float Val);

[Component]
struct Player {}
```

And to add these components to entities you can either use ``SpawnWith`` to create an entity and add components at the same time, or you can use ``AddComponent`` to add components later
```cs
// using SpawnWith
var entity = world.SpawnWith(new Component1() {
    val = 0.1f, 
}, new Component2(0.1f));

// using AddComponent
var entity = world.Spawn();

world.AddComponent(entity, new Component1() {
    val = 0.1f,
});
```

You can also remove components as such
```cs
world.RemoveComponent<Component1>(entity);
```