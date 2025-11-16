# Singletons

ECS's can be limiting when it comes to a global state, Singletons are the solution.
Singletons are data containers that can be accessed at all times as long as you are able to access ``World``

Singletons can be created using a regular class as such
```cs
class TestSingleton {
    public float SomeGlobalData;
}
```

And to use the singleton you can use
```cs
// set the singleton
world.SetSingleton(new TestSingleton() {
    SomeGlobalData = 1f;
});

// fetch the singleton
ref var testSingleton = world.GetSingleton<TestSingleton>();
testSingleton.SomeGlobalData = 2f; 
```