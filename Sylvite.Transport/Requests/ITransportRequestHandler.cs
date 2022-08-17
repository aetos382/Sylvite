namespace Sylvite.Transport;

public interface ITransportRequestHandler
{
    GetObjectResponse HandleGetObject(
        GetObjectRequest command,
        RequestContext context);
}
