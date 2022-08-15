namespace Sylvite.Transport;

public interface ITransportRequest
{
    void Handle(
        ITransportRequestHandler handler,
        RequestContext context);
}

public interface ITransportRequest<TResponse>
    where TResponse : ITransportResponse
{
}