
/*
 * Created a class for DomainExceptions. 
 * In this class I create various constructors, one of which is built so that I can combine a DomainException with, for example, an ArgumentException in the Service layer.
 */

namespace EducationPlatform.Domain.Middlewares;

public class DomainException : Exception
{
    public DomainException() : base() { }

    public DomainException(string message) : base(message) { }

    public DomainException(string message, Exception innerException) : base(message, innerException) { }
}
