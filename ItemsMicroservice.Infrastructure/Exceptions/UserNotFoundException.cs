using ItemsMicroservice.Core.Exceptions;

namespace ItemsMicroservice.Infrastructure.Exceptions;

public class UserNotFoundException : ItemsMicroserviceException
{
    public UserNotFoundException(string identifier) 
        : base($"User identified by: '{identifier}' was not found.")
    {
    }
}