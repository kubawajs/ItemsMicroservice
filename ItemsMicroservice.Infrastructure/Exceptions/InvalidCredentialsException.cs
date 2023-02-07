using ItemsMicroservice.Core.Exceptions;

namespace ItemsMicroservice.Infrastructure.Exceptions;

public class InvalidCredentialsException : ItemsMicroserviceException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}