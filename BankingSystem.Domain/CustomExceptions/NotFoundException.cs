using System.Net;

namespace Domain.CustomExceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message, HttpStatusCode.NotFound) {
    }
}