using Sylvite.Loader;

using System.Runtime.CompilerServices;

namespace Sylvite.Visualizer;

internal static class Module
{
#pragma warning disable CA2255

    [ModuleInitializer]
    public static void Initialize()
    {
        var assemblyLoader = new AssemblyLoader("Sylvite.Visualizer");
        assemblyLoader.Hook();
    }

#pragma warning restore CA2255
}
