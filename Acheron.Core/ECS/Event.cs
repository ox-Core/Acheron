namespace Acheron.Core.ECS;

[AttributeUsage(AttributeTargets.Delegate | AttributeTargets.Method)]
public class SubscribeAttribute : Attribute {

    public Type EventType;

    public SubscribeAttribute(Type eventType) => EventType = eventType;
}