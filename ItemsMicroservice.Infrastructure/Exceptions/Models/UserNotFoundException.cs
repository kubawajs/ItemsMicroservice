using ItemsMicroservice.Core.Exceptions;

namespace ItemsMicroservice.Infrastructure.Exceptions.Models;

public class UserNotFoundException : ItemsMicroserviceException
{
    public UserNotFoundException(string identifier)
        : base($"User identified by: '{identifier}' was not found.")
    {
    }
}