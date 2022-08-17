using System;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.DebuggerVisualizers;

using Sylvite.Debuggee;
using Sylvite.Visualizer;

namespace Sylvite.TestConsole;

internal static class Program
{
    [STAThread]
    private static void Main()
    {
        var obj = CSharpSyntaxTree.ParseText("[A][B,C]class Foo: object {}");

        var host = new VisualizerDevelopmentHost(
            obj,
            typeof(SyntaxVisualizer),
            typeof(SyntaxTransportSource),
            false);

        host.ShowVisualizer();

        var host2 = new VisualizerDevelopmentHost(
            obj,
            typeof(SyntaxVisualizer),
            typeof(SyntaxTransportSource),
            false);

        host2.ShowVisualizer();
    }
}
