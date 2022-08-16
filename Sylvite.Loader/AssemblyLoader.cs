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

    private static readonly IReadOnlyList<string> DefaultExtensions = new[] { ".dll" };

    public AssemblyLoader(
        IReadOnlyList<string> provingPaths,
        IReadOnlyList<string>? extensions = null)
    {
        extensions = (extensions ?? DefaultExtensions)
            .Select(x =>
            {
                var e = x.TrimStart('*');

                if (e.Length == 0)
                {
                    e = "*";
                }

                if (e[0] != '.')
                {
                    e = $".{e}";
                }

                return e;
            })
            .ToArray();

        var basePath = Path.GetDirectoryName(Assembly.GetCallingAssembly().Location);

        var provingPathList = provingPaths
            .Select(x => Path.Combine(basePath, x))
            .Where(static x => Directory.Exists(x))
            .Select(static (x, i) => (Index: i, Path: x))
            .ToList();

        provingPathList.Insert(0, (Index: 0, Path: basePath));

        var candidateFiles = provingPathList
            .SelectMany(x =>
                extensions.Select(e => (x.Index, x.Path, Extension: e)))
            .SelectMany(static x =>
            {
                var files = Directory.EnumerateFiles(x.Path, $"*.{x.Extension}", SearchOption.AllDirectories);
                return files
                    .Where(f => Path.GetExtension(f).Equals(x.Extension, StringComparison.OrdinalIgnoreCase))
                    .Select(f => (
                        x.Index,
                        Path: f,
                        Name: Path.GetFileNameWithoutExtension(x.Path)));
            })
            .GroupBy(static x => x.Name)
            .Select(static x => (Name: x.Key, Path: x.OrderBy(static x => x.Index).First().Path))
            .Where(static x =>
            {
#pragma warning disable CA1031
                try
                {
                    var name = AssemblyName.GetAssemblyName(x.Path);
                    return string.Equals(name.Name, x.Name, StringComparison.OrdinalIgnoreCase);
                }
                catch
                {
                    return false;
                }
#pragma warning restore CA1031
            })
            .ToDictionary(
                x => x.Name,
                x => x.Path,
                StringComparer.OrdinalIgnoreCase);

        this._candidateFiles = candidateFiles;
    }

    public void Hook()
    {
        AppDomain.CurrentDomain.AssemblyResolve += (_, e) =>
        {
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
