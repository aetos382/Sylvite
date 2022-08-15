using System;

using Sylvite.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class GetObjectRequest :
    ITransportRequest,
    ITransportRequest<GetObjectResponse>
{
    public GetObjectRequest(
        int count)
    {
        Guard.GreaterThanOrEqualTo(count, -1);

        this.Count = count;
    }

    public int Count { get; }

    public void Handle(
        ITransportRequestHandler handler,
        RequestContext context)
    {
        Guard.NotNull(handler);
        Guard.NotNull(context);

        handler.OnGetObject(this, context);
    }
}
