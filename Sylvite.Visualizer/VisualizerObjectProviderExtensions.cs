using Microsoft.VisualStudio.DebuggerVisualizers;

using CommunityToolkit.Diagnostics;

using Sylvite.Transport;

namespace Sylvite.Visualizer;

internal static class VisualizerObjectProviderExtensions
{
    public static TResponse SendRequest<TRequest, TResponse>(
        this IVisualizerObjectProvider2 provider,
        TRequest request)
        where TRequest : ITransportRequest
        where TResponse : ITransportResponse
    {
        Guard.IsNotNull(provider);
        Guard.IsNotNull(request);

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
