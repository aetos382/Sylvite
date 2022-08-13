using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Sylvite.Diagnostics;
using Sylvite.Transport;

namespace Sylvite.SyntaxTranslator;

public partial class SyntaxConverter :
    CSharpSyntaxVisitor<SyntaxTransport?>,
    IObservable<SyntaxTransport>
{
    private readonly SyntaxWalkerDepth _depth;

    private readonly List<IObserver<SyntaxTransport>> _observers = new();

    public SyntaxConverter(
        SyntaxWalkerDepth depth = SyntaxWalkerDepth.Node)
    {
        this._depth = depth;
    }

    private readonly Stack<SyntaxTransport> _branch = new();

    public IDisposable Subscribe(
        IObserver<SyntaxTransport> observer)
    {
        Guard.NotNull(observer);

        this._observers.Add(observer);

        return Subscription.Create(
            static state => {
                state.Observers.Remove(state.Observer);
            },
            (Observers: this._observers, Observer: observer));
    }

    private void OnTraverse(
        SyntaxTransport transport)
    {
        foreach (var observer in this._observers)
        {
            observer.OnNext(transport);
        }
    }

    public void TraverseCompleted()
    {
        foreach (var observer in this._observers)
        {
            observer.OnCompleted();
        }
    }

    private delegate SyntaxTransport? TransportFactory<in T>(
        T node,
        SyntaxTransport? parent)
        where T :
            CSharpSyntaxNode;

    private delegate IEnumerable<SyntaxTransport?> ChildGenerator<in T>(
        SyntaxConverter self,
        T node,
        SyntaxTransport transport)
        where T :
            CSharpSyntaxNode;

    private SyntaxTransport? ProcessNode<T>(
        T node,
        TransportFactory<T> createTransport,
        ChildGenerator<T> childGenerator)
        where T :
            CSharpSyntaxNode
    {
        _ = this._branch.TryPeek(out var parent);

        var transport = createTransport(node, parent);
        if (transport is null)
        {
            return null;
        }

        this._branch.Push(transport);

        try
        {
            this.OnTraverse(transport);

            var children = childGenerator(this, node, transport);
            foreach (var child in children)
            {
                if (child is not null)
                {
                    transport.AddChild(child);
                }
            }
        }
        finally
        {
            this._branch.Pop();
        }

        return transport;
    }
}
