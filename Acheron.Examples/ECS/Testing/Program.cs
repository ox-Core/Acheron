namespace Testing;

using Acheron.Core.ECS;

class BaseModule : Module {
    public override void Register(World world) {
        Console.WriteLine("BaseModule");
    }
}

class ParentModule1 : Module {
    public override Type[] Dependencies { get; }  = [typeof(BaseModule)];

    public override void Register(World world) {
        Console.WriteLine("ParentModule1");
    }
}

class ParentModule2 : Module {
    public override Type[] Dependencies { get; }  = [typeof(BaseModule)];

    public override void Register(World world) {
        Console.WriteLine("ParentModule2");
    }
}

class GrandparentModule1 : Module {
    public override Type[] Dependencies { get; }  = [typeof(ParentModule1), typeof(ParentModule2)];

    public override void Register(World world) {
        Console.WriteLine("GrandparentModule1");
    }
}


class Program {

    static void Main(string[] args) {
        var world = new World();

        world.ImportModule<ParentModule1>();        
        world.ImportModule<GrandparentModule1>();        
    }
}
