# Systems

Systems are functions that iterate over every entity that matches a query, and they also abide by the system stage.

Systems are defined using attributes. Some examples of systems are below

```cs
// this sytem will run once every frame
[System]
void EveryFrameSystem(World world) {}

// this system will run once on every entity that matches the query.
// NOTE: this query is not strict, an entity that has Component1 and Component2 will still fit this query
[System<Component1>]
void OverComponent1(World world, Entity entity, ref Component1 component1) {}
```

## Stages
The stages define the order that systems will execute in. The default stages are in order as follows
- #### **Start** (runs only once on the **FIRST** update call)
- #### **PreUpdate**
- #### **Update**
- #### **PostUpdate**

By default Systems are added to the ``Update`` stage. To specify the stage you can do as follows
```cs
// will run before every system in "Update"
[System("PreUpdate")]
void PreUpdateSystem(World world) {}

// this system will run once on the first world.Update() call
[System("Start")]
void SetupSystem(World world) {}
```

To add new stages you can use the following functions
```cs
// add the stage PrePreUpdate before PreUpdate
world.AddStageBefore("PrePreUpdate", "PreUpdate");

// add the stage PostPostUpdate after PostUpdate
world.AddStageAfter("PostPostUpdate", "PostUpdate");
```

