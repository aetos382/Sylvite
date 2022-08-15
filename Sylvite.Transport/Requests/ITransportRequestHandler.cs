namespace Sylvite.Transport;

public interface ITransportRequestHandler
{
    void OnGetObject(
        GetObjectRequest command,
        RequestContext context);
}
