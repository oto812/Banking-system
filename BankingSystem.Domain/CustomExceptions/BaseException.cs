using System.Net;

namespace Domain.CustomExceptions;

public abstract class BaseException : Exception
{
    public HttpStatusCode StatusCode { get; }

    protected BaseException(string message, HttpStatusCode statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}
