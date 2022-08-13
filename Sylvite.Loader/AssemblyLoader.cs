using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sylvite.Loader;

public class AssemblyLoader
{
    public AssemblyLoader(
        string provingPath)
        : this(
            new[] { provingPath },
            null)
    {
    }

    public AssemblyLoader(
        string[] provingPaths,
        string[]? extensions = null)
    {
        extensions ??= new[] { "*.dll" };

        var basePath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

        var provingPathList = provingPaths
            .Select((x, i) => {
                var path = Path.Combine(basePath, x);
                return (Index: i + 1, Path: path);
            })
            .Where(static x => Directory.Exists(x.Path))
            .ToList();

        provingPathList.Insert(0, (Index: 0, Path: basePath));

        var candidateFiles = provingPathList
            .SelectMany(x => extensions.Select(e => (x.Index, x.Path, Extension: e)))
            .SelectMany(static x => {
                var files = Directory.EnumerateFiles(x.Path, x.Extension, SearchOption.AllDirectories);
                return files
                    .Where(x => Path.GetExtension(x).Equals(".dll", StringComparison.OrdinalIgnoreCase))
                    .Select(f => (x.Index, Path: f));
            })
            .Select(static x => (
                x.Index,
                x.Path,
                Name: Path.GetFileNameWithoutExtension(x.Path)))
            .GroupBy(static x => x.Name)
            .Select(static x => (Name: x.Key, Path: x.OrderBy(static x => x.Index).First().Path))
            .Where(static x => {
                try
                {
                    _ = AssemblyName.GetAssemblyName(x.Path);
                    return true;
                }
                catch
                {
                    return false;
                }
            })
            .ToDictionary(
                x => x.Name,
                x => x.Path,
                StringComparer.OrdinalIgnoreCase);

        this._candidateFiles = candidateFiles;
    }

    public void Hook()
    {
        AppDomain.CurrentDomain.AssemblyResolve += (_, e) => {

            var loadedAssembly = AppDomain.CurrentDomain
                .GetAssemblies()
                .FirstOrDefault(a => a.FullName == e.Name);

            if (loadedAssembly is not null)
            {
                return loadedAssembly;
            }

            if (!this._candidateFiles.TryGetValue(e.Name, out var file))
            {
                return null;
            }

            var assemblyFound = Assembly.LoadFile(file);
            return assemblyFound;
        };
    }

    private readonly Dictionary<string, string> _candidateFiles;
}
