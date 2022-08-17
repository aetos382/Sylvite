using System.IO;

using Microsoft.VisualStudio.DebuggerVisualizers;

using CommunityToolkit.Diagnostics;

using Sylvite.Transport;

namespace Sylvite.Debuggee;

public class SyntaxTransportSource :
    VisualizerObjectSource,
    ITransportRequestHandler
{
    public override void TransferData(
        object target,
        Stream incomingData,
        Stream outgoingData)
    {
        var command = GetDeserializableObject(incomingData).ToObject<ITransportRequest>();

        var response = command.Handle(this, target);
        Serialize(outgoingData, response);
    }

    public GetObjectResponse HandleGetObject(
        GetObjectRequest command,
        object objectToVisualize)
    {
        Guard.IsNotNull(command);
        Guard.IsNotNull(objectToVisualize);

        return new GetObjectResponse();
    }
}
