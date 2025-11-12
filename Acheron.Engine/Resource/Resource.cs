using Acheron.Core.ECS;
using Acheron.Engine.Resource.Resources;
using Acheron.Engine.Types;

namespace Acheron.Engine.Resource;

public class ResourceModule : Module {

    public override void Register(World world) {
        world.SetSingleton(new ResourceManager(world));
    }
}

public class ResourceManager {
    World? world = null;

    public ResourceManager() { }
    public ResourceManager(World world) {
        this.world = world;
    }

    public static ref ResourceManager Get(World world) {
        return ref world.GetSingleton<ResourceManager>();
    }

    readonly Texture2DResourceManager texture2D = new();

    public Texture2D LoadTexture(string path) {
        if (world is null)
            throw new InvalidOperationException("Used ResourceManager with a null World");
        return texture2D.Load(world, path);
    }
}