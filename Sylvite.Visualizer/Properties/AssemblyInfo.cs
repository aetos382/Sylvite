using System.Diagnostics;

using Sylvite.Visualizer;

[assembly: DebuggerVisualizer(
    visualizer: typeof(SyntaxVisualizer),
    visualizerObjectSourceTypeName: "Sylvite.Debuggee.SyntaxTransportSource, Sylvite.Debuggee",
    TargetTypeName = "Microsoft.CodeAnalysis.CSharp.CSharpSyntaxTree, Microsoft.CodeAnalysis.CSharp, Version=4.2.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]

[assembly: DebuggerVisualizer(
    visualizer: typeof(SyntaxVisualizer),
    visualizerObjectSourceTypeName: "Sylvite.Debuggee.SyntaxTransportSource, Sylvite.Debuggee",
    TargetTypeName = "Microsoft.CodeAnalysis.CSharp.CSharpSyntaxNode, Microsoft.CodeAnalysis.CSharp, Version=4.2.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
