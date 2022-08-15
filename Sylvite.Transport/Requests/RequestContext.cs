using System.IO;

namespace Sylvite.Transport;

public record RequestContext(
    object VisualizeTarget,
    Stream IncomingData,
    Stream OutgoingData);
