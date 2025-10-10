namespace Acheron.Core.ECS.Internal;

public static class SingletonStorage<T> where T : new() {
    private static T _instance = default!;
    private static bool _initialized = false;

    public static void Set(T value) {
        _instance = value;
        _initialized = true;
    }

    public static ref T Get() {
        if (!_initialized)
            throw new InvalidOperationException("Singleton not set");

        return ref _instance;
    }

    public static bool IsSet => _initialized;
}