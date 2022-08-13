using System;

namespace Sylvite.SyntaxTranslator;

internal static class Subscription
{
    public static Subscription<T> Create<T>(
        Action<T> onDispose,
        T state)
    {
        return new Subscription<T>(onDispose, state);
    }
}

internal class Subscription<T> :
    IDisposable
{
    private readonly Action<T> _onDispose;
    private readonly T _state;
    private bool _disposed;

    public Subscription(
        Action<T> onDispose,
        T state)
    {
        this._onDispose = onDispose;
        this._state = state;
    }

    public void Dispose()
    {
        if (this._disposed)
        {
            return;
        }

        this._disposed = true;
        this._onDispose(this._state);
    }
}
