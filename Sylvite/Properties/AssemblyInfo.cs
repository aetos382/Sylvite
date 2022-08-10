using System;
using System.Diagnostics;

using Sylvite;
using Sylvite.Contract;

[assembly: CLSCompliant(false)]

[assembly: DebuggerVisualizer(
    visualizer: typeof(Visualizer),
    visualizerObjectSource: typeof(SyntaxTreeSource),
    TargetTypeName = "Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree")]
