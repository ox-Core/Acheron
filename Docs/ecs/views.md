# Views

A view is a way to query the worlds components without having to use systems.
Views take in the same arguments that a system would but in the form of a lambda. A view will iterate every component that loosly comforms to the lambdas signature.

For example, below this query will iterate through every entity that has the Components ``Component1`` and ``Component2``. It isnt strict as the entity can also have a ``Component3`` but still conform to the view, it just wont be passed as an arguement. To be able to make these stricter, try using tags as shown in [Components](components.md)

```cs
world.View((World world, Entity e, ref Component1 component, ref Component2 componrent) => {
    // do stuff here
});
```