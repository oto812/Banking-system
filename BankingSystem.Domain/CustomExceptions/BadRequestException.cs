using System.Net;

namespace Domain.CustomExceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(string message) : base(message, HttpStatusCode.BadRequest)
    {
    }
}