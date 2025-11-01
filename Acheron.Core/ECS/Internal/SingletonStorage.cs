namespace Acheron.Core.ECS.Internal;

public static class SingletonStorage<T> where T : new() {
    private static T instance = default!;
    private static bool initialized = false;

    public static void Set(T value) {
        instance = value;
        initialized = true;
    }

    public static ref T Get() {
        if (!initialized)
            throw new InvalidOperationException("Singleton not set");

        return ref instance;
    }

    public static bool IsSet => initialized;
}