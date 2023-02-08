using ItemsMicroservice.Core.Exceptions;

namespace ItemsMicroservice.Infrastructure.Exceptions.Models;

public class InvalidCredentialsException : ItemsMicroserviceException
{
    public InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}