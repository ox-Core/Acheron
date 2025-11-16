# Modules

Modules are classes that inhert from the ``Module`` base class.
When a module is imported, its ``Register(World world)`` function is called.
Modules are the primary way that you will use third party libraries, and built in functionality in ``Acheron.Engine``.

To create a new module you can do this
```cs
class TestModule : Module {
    public override void Register(World world) {
        // do stuff here
    }
}
```

And to import such module you can
```cs
world.ImportModule<TestModule>();
```

