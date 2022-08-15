using System;
using System.Collections.Generic;

namespace Sylvite.Transport;

[Serializable]
public class GetObjectResponse :
    ITransportResponse
{
    public IReadOnlyList<SyntaxTransport> Transports { get; }
    public bool Completed { get; }

    public GetObjectResponse(
        IReadOnlyList<SyntaxTransport> transports,
        bool completed)
    {
        this.Transports = transports;

        this.Completed = completed;
    }
}
