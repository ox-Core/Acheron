namespace Acheron.Core.ECS;

public class Stage
{
    public string Name { get; }
    public List<System> Systems { get; } = [];
    public Stage(string name) => Name = name;
}