using Microsoft.VisualStudio.DebuggerVisualizers;

using Sylvite.Diagnostics;
using Sylvite.Transport;

namespace Sylvite.Visualizer;

internal static class VisualizerObjectProviderExtensions
{
    public static TResponse SendRequest<TRequest, TResponse>(
        this IVisualizerObjectProvider2 provider,
        TRequest request)
        where TRequest :
            class,
            ITransportRequest,
            ITransportRequest<TResponse>
        where TResponse :
            class,
            ITransportResponse
    {
        Guard.NotNull(provider);
        Guard.NotNull(request);

        var obj = provider.TransferDeserializableObject(request);
        var response = obj.ToObject<TResponse>();

        return response;
    }

    public static GetObjectResponse GetObject(
        this IVisualizerObjectProvider2 provider,
        GetObjectRequest request)
    {
        return SendRequest<GetObjectRequest, GetObjectResponse>(provider, request);
    }
}
