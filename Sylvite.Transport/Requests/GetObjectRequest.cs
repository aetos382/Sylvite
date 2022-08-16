using System;

using Sylvite.Diagnostics;

namespace Sylvite.Transport;

[Serializable]
public class GetObjectRequest :
    ITransportRequest,
    ITransportRequest<GetObjectResponse>
{
    public GetObjectRequest(
        int chunkSize)
    {
        Guard.GreaterThanOrEqualTo(chunkSize, -1);

        this.ChunkSize = chunkSize;
    }

    public int ChunkSize { get; }

    public void Handle(
        ITransportRequestHandler handler,
        RequestContext context)
    {
        Guard.NotNull(handler);
        Guard.NotNull(context);

        handler.OnGetObject(this, context);
    }
}
