using System.Collections.Generic;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using CommunityToolkit.Diagnostics;

using Sylvite.RoslynHelper;
using Sylvite.Transport;

namespace Sylvite.Visualizer.Rebuilder;

internal class SyntaxTransportVisitor :
    ITransportVisitor<CSharpSyntaxNode>
{
    public CSharpSyntaxNode VisitClassDeclaration(
        ClassDeclarationTransport transport)
    {
        Guard.IsNotNull(transport);

        var node = SyntaxFactory.ClassDeclaration(transport.Name);

        var members = new List<MemberDeclarationSyntax>();

        foreach (var child in transport.ChildNodes)
        {
            var childNode = child.Accept(this);
            if (childNode is MemberDeclarationSyntax member)
            {
                members.Add(member);
            }
        }

        node = node.WithMembers(members.ToSyntaxList());
        return node;
    }

    public CSharpSyntaxNode VisitCompilationUnit(
        CompilationUnitTransport transport)
    {
        var node = SyntaxFactory.CompilationUnit();

        var members = new List<MemberDeclarationSyntax>();

        foreach (var child in transport.ChildNodes)
        {
            var childNode = child.Accept(this);
            if (childNode is MemberDeclarationSyntax member)
            {
                members.Add(member);
            }
        }

        node = node.WithMembers(members.ToSyntaxList());
        return node;
    }

    public CSharpSyntaxNode VisitFileScopedNamespaceDeclaration(
        FileScopedNamespaceDeclarationTransport transport)
    {
        var node = SyntaxFactory.FileScopedNamespaceDeclaration(
            SyntaxFactory.IdentifierName(transport.Name));

        var members = new List<MemberDeclarationSyntax>();

        foreach (var child in transport.ChildNodes)
        {
            var childNode = child.Accept(this);
            if (childNode is MemberDeclarationSyntax member)
            {
                members.Add(member);
            }
        }

        node = node.WithMembers(members.ToSyntaxList());
        return node;
    }

    public CSharpSyntaxNode VisitNamespaceDeclaration(
        NamespaceDeclarationTransport transport)
    {
        var node = SyntaxFactory.NamespaceDeclaration(
            SyntaxFactory.IdentifierName(transport.Name));

        var members = new List<MemberDeclarationSyntax>();

        foreach (var child in transport.ChildNodes)
        {
            var childNode = child.Accept(this);
            if (childNode is MemberDeclarationSyntax member)
            {
                members.Add(member);
            }
        }

        node = node.WithMembers(members.ToSyntaxList());
        return node;
    }
}
