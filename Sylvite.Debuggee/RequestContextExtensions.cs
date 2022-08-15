using Microsoft.VisualStudio.DebuggerVisualizers;

using Sylvite.Diagnostics;
using Sylvite.Transport;

namespace Sylvite.Debuggee;

internal static class RequestContextExtensions
{
    public static void SendObject(
        this RequestContext context,
        ITransportResponse objectToSend)
    {
        Guard.NotNull(context);
        Guard.NotNull(objectToSend);

        VisualizerObjectSource.Serialize(context.OutgoingData, objectToSend);
    }
}
