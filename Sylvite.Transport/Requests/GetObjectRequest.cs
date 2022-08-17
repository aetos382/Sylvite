using System;

using CommunityToolkit.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class GetObjectRequest :
    ITransportRequest
{
    public ITransportResponse Handle(
        ITransportRequestHandler handler,
        object objectToVisualize)
    {
        Guard.IsNotNull(handler);
        Guard.IsNotNull(objectToVisualize);

        return handler.HandleGetObject(this, objectToVisualize);
    }
}
