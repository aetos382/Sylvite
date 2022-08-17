namespace Sylvite.Transport;

public interface ITransportRequest
{
    ITransportResponse Handle(
        ITransportRequestHandler handler,
        RequestContext context);
}
