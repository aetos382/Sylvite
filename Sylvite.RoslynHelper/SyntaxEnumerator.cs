using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp;

namespace Sylvite.RoslynHelper;

public record VisitResult<TResult>(
    TResult Result,
    IEnumerable<CSharpSyntaxNode> NextNodes);

public abstract class SyntaxEnumerator<TResult> :
    IEnumerable<TResult>
{
    private readonly CSharpSyntaxNode _initialElement;

    protected SyntaxEnumerator(
        CSharpSyntaxNode initialElement)
    {
        this._initialElement = initialElement;
    }

    public IEnumerator<TResult> GetEnumerator()
    {
        return this.CreateVisitor();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    protected abstract InternalVisitor CreateVisitor();

    protected abstract class InternalVisitor :
        CSharpSyntaxVisitor<VisitResult<TResult>>,
        IEnumerator<TResult>
    {
        private readonly SyntaxEnumerator<TResult> _visitor;
        private readonly List<IEnumerator<CSharpSyntaxNode>> _enumerators = new();
        private readonly Stack<TResult> _branch = new();
        private bool _disposed;

        protected InternalVisitor(
            SyntaxEnumerator<TResult> visitor)
        {
            this._visitor = visitor;
        }

        public bool MoveNext()
        {
            this.CheckDisposed();

            if (!this._branch.Any())
            {
                return this.SetCurrentAndAddEnumerator(this._visitor._initialElement);
            }

            var enumerators = this._enumerators;
            for (var i = enumerators.Count - 1; i >= 0; --i)
            {
                var enumerator = enumerators[i];
                while (enumerator.MoveNext())
                {
                    if (!this.SetCurrentAndAddEnumerator(enumerator.Current))
                    {
                        continue;
                    }

                    return true;
                }

                this._branch.Pop();

                enumerator.Dispose();
                enumerators.RemoveAt(i);
            }

            return false;
        }

        public void Reset()
        {
            this.CheckDisposed();

            this._branch.Clear();
            this.DisposeEnumerator();
        }

        public TResult Current
        {
            get
            {
                this.CheckDisposed();

                if (!this._branch.TryPeek(out var current))
                {
                    throw new InvalidOperationException();
                }

                return current;
            }
        }

        object? IEnumerator.Current
        {
            get
            {
                return this.Current;
            }
        }

        protected TResult? Parent
        {
            get
            {
                _ = this._branch.TryPeek(out var parent);
                return parent;
            }
        }

        protected IEnumerable<TResult> Branch
        {
            get
            {
                return this._branch;
            }
        }

        protected int Depth
        {
            get
            {
                return this._branch.Count;
            }
        }

        public void Dispose()
        {
            if (this._disposed)
            {
                return;
            }

            this._disposed = true;

            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(
            bool disposing)
        {
            if (disposing)
            {
                this.DisposeEnumerator();
            }
        }

        private void CheckDisposed()
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(nameof(InternalVisitor));
            }
        }

        private bool SetCurrentAndAddEnumerator(
            CSharpSyntaxNode node)
        {
            var next = this.Visit(node);

            if (next is null)
            {
                return false;
            }

            var result = next.Result;
            this._branch.Push(result);
            this._enumerators.Add(next.NextNodes.GetEnumerator());

            return true;
        }

        private void DisposeEnumerator()
        {
            var enumerators = this._enumerators;

            foreach (var enumerator in enumerators)
            {
                enumerator.Dispose();
            }

            enumerators.Clear();
        }
    }
}
