# Window Module

Before importing the module you have the option to set a ``WindowConfig`` singleton to configure said window.
```cs
world.SetSingleton(new WindowConfig() {
    Width = 1920;
    Height = 1080;
    Title = "Test";
    Resizeable = false;
});
```

When the module is imported, it will create a new GLFW window handle, set its singleton, and register systems for polling it.

To use this new windows global singleton, after registering you can fetch and use it like so
```cs
ref var window = ref world.GetSingleton<Window>();

while(!window.ShouldClose) {
    world.Update();
}
```