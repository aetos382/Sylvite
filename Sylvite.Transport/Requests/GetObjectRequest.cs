using System;

using CommunityToolkit.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class GetObjectRequest :
    ITransportRequest
{
    public ITransportResponse Handle(
        ITransportRequestHandler handler,
        RequestContext context)
    {
        Guard.IsNotNull(handler);
        Guard.IsNotNull(context);

        return handler.HandleGetObject(this, context);
    }
}
