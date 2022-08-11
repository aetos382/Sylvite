using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using Sylvite.RoslynHelper;
using Sylvite.Transport;

namespace Sylvite.Visualizer;

internal class SyntaxRebuilder :
    ITransportVisitor<CSharpSyntaxNode>
{
    public CSharpSyntaxNode BuildSyntax(
        SyntaxTransport transport)
    {
        return transport.Accept(this);
    }

    public CSharpSyntaxNode VisitCompilationUnit(
        CompilationUnitTransport transport)
    {
        var nodes = new List<CSharpSyntaxNode>(transport.Children.Count);

        foreach (var childNode in transport.Children)
        {
            var node = childNode.Accept(this);
            nodes.Add(node);
        }

        var members = nodes
            .OfType<MemberDeclarationSyntax>();

        var compilationUnit = SyntaxFactory.CompilationUnit()
            .WithMembers(members.ToSyntaxList());

        return compilationUnit;
    }

    public CSharpSyntaxNode VisitFileScopedNamespaceDeclaration(
        FileScopedNamespaceDeclarationTransport transport)
    {
        return SyntaxFactory.FileScopedNamespaceDeclaration(
            SyntaxFactory.IdentifierName(transport.Name));
    }

    public CSharpSyntaxNode VisitNamespaceDeclaration(
        NamespaceDeclarationTransport transport)
    {
        return SyntaxFactory.NamespaceDeclaration(
            SyntaxFactory.IdentifierName(transport.Name));
    }

    public CSharpSyntaxNode VisitClassDeclaration(
        ClassDeclarationTransport transport)
    {
        return SyntaxFactory.ClassDeclaration(
            transport.Name);
    }
}
