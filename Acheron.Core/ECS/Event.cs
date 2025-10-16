namespace Acheron.Core.ECS;

public class UntypedSubscribeAttribute : Attribute {

    public Type? EventType;
}

[AttributeUsage(AttributeTargets.Delegate | AttributeTargets.Method)]
public class SubscribeAttribute<T> : UntypedSubscribeAttribute {

    public SubscribeAttribute() => EventType = typeof(T);
}
