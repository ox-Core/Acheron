# Entities

Entities are really just unique identifiers that other parts of the ECS uses to associate data with one another.

By itself entities dont hold any state whatsover, the definition is just 
```cs
public readonly struct Entity {
    public ulong Value { get; }
}
```

## Creation
To create an entity you can use one of two different interfaces

First is to just spawn an empty entity
```cs
var entity = world.Spawn();
```

Second is to spawn an entity with [Components](components.md)
```cs
// you can either do this to have the ECS infer the types
var entity = world.SpawnWith(new Component1(), new Component2(), new Component3());

// or you can specify the types in the generic parameter
var entity = world.SpawnWith<Component1, Component2, Component3>(new(), new(), new());
```
