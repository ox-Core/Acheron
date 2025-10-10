using System;
using System.Collections.Generic;

namespace Acheron.Core.ECS;

public readonly struct Entity {
    public ulong Value { get; }
    public Entity(ulong value) => Value = value;
    public static implicit operator ulong(Entity e) => e.Value;
    public static implicit operator Entity(ulong v) => new(v);
}

public readonly struct ComponentID {
    public ushort Value { get; }
    public ComponentID(ushort value) => Value = value;
    public static implicit operator ushort(ComponentID c) => c.Value;
    public static implicit operator ComponentID(ushort v) => new(v);
}

public class Signature : HashSet<ComponentID> {
    public Signature() : base() { }
    public Signature(HashSet<ComponentID> signature) : base(signature) { }
    
    public bool Matches(Signature other) {
        return this.IsSubsetOf(other);
    }
    public override string ToString() => $"Signature[{string.Join(", ", this)}]";
}